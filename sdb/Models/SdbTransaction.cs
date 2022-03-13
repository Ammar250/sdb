using System;
using System.Collections.Generic;

namespace sdb.Models
{
    public partial class SdbTransaction
    {
        public int Id { get; set; }
        public int? DonorId { get; set; }
        public int? CompaignId { get; set; }
        public decimal DonationAmount { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int Csv { get; set; }
        public string CardNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public int Active { get; set; }

        public virtual SdbCompaigns Compaign { get; set; }
        public virtual SdbSystemUsers Donor { get; set; }
    }
}
