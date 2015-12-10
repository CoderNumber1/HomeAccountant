using HomeAccountant.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccountant.Core.DataRepositories
{
    public interface IHomeAccountantRepository : IDisposable
    {
        IQueryable<Account> GetAccounts();
        Account CreateAccount();

        IQueryable<AccountStatu> GetAccountStatuses();
        AccountStatu CreateAccountStatus();

        IQueryable<AccountStatusType> GetAccountStatusTypes();

        IQueryable<AccountTransaction> GetAccountTransactions();
        AccountTransaction CreateAccountTransaction();

        IQueryable<AccountType> GetAccountTypes();
        
        IQueryable<PermanentAccount> GetPermanentAccounts();

        IQueryable<vw_AccountWithStatusInfo> GetAccountStatusInfo();

        IQueryable<Account> SearchAccounts(int? accountType = null, int? parentAccountId = null, DateTime? searchDate = null, bool? permanantAccount = null);

        void Delete(object entity);

        void Save();
    }
}
