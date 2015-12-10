using HomeAccountant.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccountant.Core.DataServices
{
    public enum AccountStatus { Open, Close }

    public interface IHomeAccountantDataService : IDisposable
    {
        Account OpenAccount(string Name, AccountType type, DateTime openDate, Account parentAccount = null);
        IEnumerable<Account> GetAccounts(AccountType accountType = null, DateTime? searchDate = null, int? parentAccount = null, bool? permanentAccountsOnly = null);
        AccountTransaction RecordTransaction(DateTime transactionDate, decimal amount, Account debitAccount, Account creditAccount);
        AccountStatu CloseAccount(Account account, DateTime closeDate);

        Models.AccountType GetAccountType(Account account);

        void Save();
    }
}
