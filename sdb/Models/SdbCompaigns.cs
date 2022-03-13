using System;
using System.Collections.Generic;

namespace sdb.Models
{
    public partial class SdbCompaigns
    {
        public SdbCompaigns()
        {
            SdbPayments = new HashSet<SdbPayments>();
            SdbTransaction = new HashSet<SdbTransaction>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string CompaignPurpose { get; set; }
        public int? UserId { get; set; }
        public string Description { get; set; }
        public decimal TotalAmountNeeded { get; set; }
        public decimal? CollectedAmount { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public int Active { get; set; }

        public virtual ICollection<SdbPayments> SdbPayments { get; set; }
        public virtual ICollection<SdbTransaction> SdbTransaction { get; set; }
    }
}
