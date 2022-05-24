using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace sdb.Models
{
    public partial class SdbTransaction
    {
        public int Id { get; set; }
        public int? DonorId { get; set; }
        public int? CompaignId { get; set; }
        public long DonationAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public int Active { get; set; }

        [ForeignKey("DonorId")]
        public virtual SdbCompaigns Compaign { get; set; }
        [ForeignKey("CompaignId")]
        public virtual SdbSystemUsers Donor { get; set; }
    }
}
