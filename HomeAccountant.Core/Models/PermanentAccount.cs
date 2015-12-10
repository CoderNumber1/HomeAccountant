using System;
using System.Collections.Generic;

namespace HomeAccountant.Core.Models
{
    public partial class PermanentAccount
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public virtual Account Account { get; set; }
    }
}
