using System;
using System.Collections.Generic;

namespace HomeAccountant.Core.Models
{
    public partial class AccountTransaction
    {
        public AccountTransaction()
        {
            this.AccountTransactionNotes = new List<AccountTransactionNote>();
        }

        public int Id { get; set; }
        public System.DateTime TransactionDate { get; set; }
        public int DebitAccountId { get; set; }
        public int CreditAccountId { get; set; }
        public decimal TransactionAmount { get; set; }
        public virtual Account Account { get; set; }
        public virtual Account Account1 { get; set; }
        public virtual ICollection<AccountTransactionNote> AccountTransactionNotes { get; set; }
    }
}
