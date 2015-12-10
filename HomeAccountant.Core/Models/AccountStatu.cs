using System;
using System.Collections.Generic;

namespace HomeAccountant.Core.Models
{
    public partial class AccountStatu
    {
        public int Id { get; set; }
        public int AccountStatusTypeId { get; set; }
        public int AccountId { get; set; }
        public System.DateTime StatusDateTime { get; set; }
        public virtual Account Account { get; set; }
        public virtual AccountStatusType AccountStatusType { get; set; }
    }
}
