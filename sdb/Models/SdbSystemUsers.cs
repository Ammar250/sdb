using System;
using System.Collections.Generic;

namespace sdb.Models
{
    public partial class SdbSystemUsers
    {
        public SdbSystemUsers()
        {
            SdbPayments = new HashSet<SdbPayments>();
            SdbTransaction = new HashSet<SdbTransaction>();
            SdbCompaigns = new HashSet<SdbCompaigns>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string UserRole { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public int Active { get; set; }

        public virtual ICollection<SdbPayments> SdbPayments { get; set; }
        public virtual ICollection<SdbTransaction> SdbTransaction { get; set; }
        public virtual ICollection<SdbCompaigns> SdbCompaigns { get; set; }
    }
}
