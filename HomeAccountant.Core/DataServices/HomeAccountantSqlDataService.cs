using HomeAccountant.Core.Configuration;
using HomeAccountant.Core.DataRepositories;
using HomeAccountant.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccountant.Core.DataServices
{
    public class HomeAccountantSqlDataService : IHomeAccountantDataService
    {
        private IHomeAccountantRepository repository;

        public HomeAccountantSqlDataService(IHomeAccountantRepository repository)
        {
            this.repository = repository;
        }

        public Models.Account OpenAccount(string name, AccountType type, DateTime openDate, Models.Account parentAccount = null)
        {
            if (this.repository.GetAccounts().Any(a => a.Name.ToUpper() == name.ToUpper()))
                throw new ArgumentException("An account by that name already exists");

            if(type == null)
                throw new ArgumentException("Invalid account type");

            Models.AccountStatusType openType = this.repository.GetAccountStatusTypes()
                    .FirstOrDefault(t => t.Name.ToUpper() == AccountStatus.Open.ToString().ToUpper());
            Models.AccountStatusType closeType = this.repository.GetAccountStatusTypes()
                .FirstOrDefault(t => t.Name.ToUpper() == AccountStatus.Close.ToString().ToUpper());

            if(parentAccount != null)
            {
                Models.AccountStatu parentOpen = this.repository.GetAccountStatuses()
                    .Where(s => s.AccountStatusTypeId == openType.Id 
                        && s.AccountId == parentAccount.Id && s.StatusDateTime <= openDate)
                    .OrderByDescending(s => s.StatusDateTime)
                    .FirstOrDefault();
                Models.AccountStatu parentClose = this.repository.GetAccountStatuses()
                    .Where(s => s.AccountStatusTypeId == closeType.Id
                        && s.AccountId == parentAccount.Id)
                    .OrderByDescending(s => s.StatusDateTime)
                    .FirstOrDefault();

                if (parentOpen == null || (parentClose != null && parentClose.StatusDateTime < openDate))
                    throw new ArgumentException("Parent account was not open at the time this account was opened");

                if (parentAccount.AccountTypeId != type.Id)
                    throw new ArgumentException("Parent account type must match child account type");
            }

            Account result = this.repository.CreateAccount();
            result.Name = name;
            result.AccountType = type;

            if (parentAccount != null)
            {
                result.ParentAccountId = parentAccount.Id;
            }

            AccountStatu accountOpening = this.repository.CreateAccountStatus();
            accountOpening.StatusDateTime = openDate;

            openType.AccountStatus.Add(accountOpening);
            result.AccountStatus.Add(accountOpening);

            return result;
        }

        public IEnumerable<Models.Account> GetAccounts(AccountType accountType = null, DateTime? searchDate = null, int? parentAccount = null, bool? permanentAccountsOnly = null)
        {
            return this.repository.SearchAccounts(accountType != null ? accountType.Id : (int?)null, parentAccount,
                searchDate, permanentAccountsOnly).ToList();

            //IQueryable<vw_AccountWithStatusInfo> statusInfo = this.repository.GetAccountStatusInfo();

            //if(accountType != null)
            //    statusInfo = statusInfo.Where(i => i.AccountType == accountType.Id);

            //if(searchDate != null)
            //    statusInfo = statusInfo.Where(i => i.OpenDate != null && i.OpenDate <= searchDate
            //        && (i.CloseDate >= searchDate || i.CloseDate == null));

            //if(parentAccount != null)
            //    statusInfo = statusInfo.Where(i => i.ParentAccountId == parentAccount.Id);

            //if(permanentAccountsOnly != null)
            //    statusInfo = statusInfo.Where(i => i.IsPermenantAccount == permanentAccountsOnly);

            //IQueryable<Account> result = statusInfo.Join(this.repository.GetAccounts(),
            //    i => i.Id,
            //    a => a.Id,
            //    (i, a) => a);

            //return result.ToList();
        }

        public Models.AccountTransaction RecordTransaction(DateTime transactionDate, decimal amount, Models.Account debitAccount, Models.Account creditAccount)
        {
            AccountTransaction result = this.repository.CreateAccountTransaction();
            result.CreditAccountId = creditAccount.Id;
            result.DebitAccountId = debitAccount.Id;
            result.TransactionAmount = amount;
            result.TransactionDate = transactionDate;

            return result;
        }

        public Models.AccountType GetAccountType(Account account)
        {
            return this.repository.GetAccountTypes().FirstOrDefault(t => t.Id == account.AccountTypeId);
        }

        public void Save()
        {
            this.repository.Save();
        }

        #region IDisposable
        private bool disposed = false;
        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.repository.Dispose();
                }

                this.disposed = true;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            this.Dispose(true);
        }
        ~HomeAccountantSqlDataService()
        {
            this.Dispose(false);
        }
        #endregion IDisposable


        public AccountStatu CloseAccount(Account account, DateTime closeDate)
        {
            if (account == null)
                throw new ArgumentException("You must provide an account to close");
            if (account.IsPermanent)
                throw new ArgumentException("You cannot close a permenant account");

            Models.AccountStatusType openType = this.repository.GetAccountStatusTypes()
                    .FirstOrDefault(t => t.Name.ToUpper() == AccountStatus.Open.ToString().ToUpper());
            Models.AccountStatusType closeType = this.repository.GetAccountStatusTypes()
                .FirstOrDefault(t => t.Name.ToUpper() == AccountStatus.Close.ToString().ToUpper());

            Models.AccountStatu accountOpen = this.repository.GetAccountStatuses()
                .Where(s => s.AccountStatusTypeId == openType.Id
                    && s.AccountId == account.Id && s.StatusDateTime <= closeDate)
                .OrderByDescending(s => s.StatusDateTime)
                .FirstOrDefault();
            DateTime? openDate = accountOpen == null ? (DateTime?)null : accountOpen.StatusDateTime;

            Models.AccountStatu accountRecentClosing = this.repository.GetAccountStatuses()
                .Where(s => s.AccountStatusTypeId == closeType.Id
                    && s.AccountId == account.Id
                    && (openDate == null || openDate < s.StatusDateTime))
                .OrderByDescending(s => s.StatusDateTime)
                .FirstOrDefault();

            if (accountOpen == null || (accountRecentClosing != null && accountRecentClosing.StatusDateTime < closeDate))
                throw new ArgumentException("The account is already closed or hasn't been opened");

            AccountStatu accountClosing = this.repository.CreateAccountStatus();
            accountClosing.StatusDateTime = closeDate;

            accountClosing.AccountStatusTypeId = closeType.Id;
            accountClosing.AccountId = account.Id;
            
            return accountClosing;
        }
    }
}
