using System;
using System.Collections.Generic;

namespace HomeAccountant.Core.Models
{
    public partial class AccountTransactionNote
    {
        public int Id { get; set; }
        public int AccountTransactionId { get; set; }
        public int OrderBy { get; set; }
        public string Note { get; set; }
        public virtual AccountTransaction AccountTransaction { get; set; }
    }
}
