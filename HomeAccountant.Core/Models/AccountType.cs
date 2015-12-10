using System;
using System.Collections.Generic;

namespace HomeAccountant.Core.Models
{
    public partial class AccountType
    {
        public AccountType()
        {
            this.Accounts = new List<Account>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
    }
}
