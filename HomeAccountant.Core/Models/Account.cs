using System;
using System.Collections.Generic;

namespace HomeAccountant.Core.Models
{
    public partial class Account
    {
        public Account()
        {
            this.Account1 = new List<Account>();
            this.AccountStatus = new List<AccountStatu>();
            this.AccountTransactions = new List<AccountTransaction>();
            this.AccountTransactions1 = new List<AccountTransaction>();
            this.PermanentAccounts = new List<PermanentAccount>();
        }

        public int Id { get; set; }
        public int AccountTypeId { get; set; }
        public string Name { get; set; }
        public Nullable<int> ParentAccountId { get; set; }
        public bool IsPermanent { get; set; }
        public virtual ICollection<Account> Account1 { get; set; }
        public virtual Account Account2 { get; set; }
        public virtual AccountType AccountType { get; set; }
        public virtual ICollection<AccountStatu> AccountStatus { get; set; }
        public virtual ICollection<AccountTransaction> AccountTransactions { get; set; }
        public virtual ICollection<AccountTransaction> AccountTransactions1 { get; set; }
        public virtual ICollection<PermanentAccount> PermanentAccounts { get; set; }
    }
}
