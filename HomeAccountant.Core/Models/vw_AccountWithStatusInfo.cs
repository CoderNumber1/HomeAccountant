using System;
using System.Collections.Generic;

namespace HomeAccountant.Core.Models
{
    public partial class vw_AccountWithStatusInfo
    {
        public int Id { get; set; }
        public Nullable<int> ParentAccountId { get; set; }
        public int AccountType { get; set; }
        public System.DateTime OpenDate { get; set; }
        public Nullable<System.DateTime> CloseDate { get; set; }
        public bool IsPermenantAccount { get; set; }
    }
}
