using System;
using System.Collections.Generic;

namespace HomeAccountant.Core.Models
{
    public partial class AccountStatusType
    {
        public AccountStatusType()
        {
            this.AccountStatus = new List<AccountStatu>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<AccountStatu> AccountStatus { get; set; }
    }
}
