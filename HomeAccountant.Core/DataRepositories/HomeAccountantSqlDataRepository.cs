using HomeAccountant.Core.Configuration;
using HomeAccountant.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccountant.Core.DataRepositories
{
    public class HomeAccountantSqlDataRepository : IHomeAccountantRepository
    {
        private HomeAccountantContext context;

        public HomeAccountantSqlDataRepository(IConnectionConfiguration config)
        {
            this.context = new HomeAccountantContext(config.HomeAccountantConnectionString);
        }

        public IQueryable<Account> GetAccounts()
        {
            return this.context.Accounts;
        }

        public Account CreateAccount()
        {
            return this.context.Accounts.Add(this.context.Accounts.Create());
        }

        public IQueryable<AccountStatu> GetAccountStatuses()
        {
            return this.context.AccountStatus;
        }

        public AccountStatu CreateAccountStatus()
        {
            return this.context.AccountStatus.Add(this.context.AccountStatus.Create());
        }

        public IQueryable<AccountStatusType> GetAccountStatusTypes()
        {
            return this.context.AccountStatusTypes;
        }

        public IQueryable<AccountTransaction> GetAccountTransactions()
        {
            return this.context.AccountTransactions;
        }

        public AccountTransaction CreateAccountTransaction()
        {
            return this.context.AccountTransactions.Add(this.context.AccountTransactions.Create());
        }

        public IQueryable<AccountType> GetAccountTypes()
        {
            return this.context.AccountTypes;
        }

        public IQueryable<PermanentAccount> GetPermanentAccounts()
        {
            return this.context.PermanentAccounts;
        }

        public IQueryable<vw_AccountWithStatusInfo> GetAccountStatusInfo()
        {
            return this.context.vw_AccountWithStatusInfo;
        }

        public void Delete(object entity)
        {
            this.context.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
        }

        public void Save()
        {
            this.context.SaveChanges();
        }

        #region IDisposable
        private bool disposed = false;
        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.context.Dispose();
                }

                this.disposed = true;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            this.Dispose(true);
        }
        ~HomeAccountantSqlDataRepository()
        {
            this.Dispose(false);
        }
        #endregion IDisposable


        public IQueryable<Account> SearchAccounts(int? accountType = null, int? parentAccountId = null, DateTime? searchDate = null, bool? permanantAccount = null)
        {
            return this.context.SearchAccounts(accountType, parentAccountId, searchDate, permanantAccount);
        }
    }
}
