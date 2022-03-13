using System;
using System.Collections.Generic;

namespace sdb.Models
{
    public partial class SdbPayments
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public decimal Amount { get; set; }
        public int? CompaignId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public int Active { get; set; }

        public virtual SdbCompaigns Compaign { get; set; }
        public virtual SdbSystemUsers User { get; set; }
    }
}
