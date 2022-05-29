using DotLiquid.Tags;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sdb.Models
{
    public partial class SdbCompaigns
    {
        public SdbCompaigns()
        {
            SdbPayments = new HashSet<SdbPayments>();
            SdbTransaction = new HashSet<SdbTransaction>();
            sdbSystemUsers = new SdbSystemUsers();
        }

        public int Id { get; set; }
        [Required(ErrorMessage ="Name is required")]
        [StringLength(60, MinimumLength = 3)]
        public string Name { get; set; }
        
        public string Image { get; set; }
        public string CompaignPurpose { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Total Amount is required")]
        [RegularExpression(@"^([1-9][0-9]{1,7})$", ErrorMessage = "Total Amount must be between 1 - 99999999.")]
        [Display(Name = "Total Amount Needed")]
        public long TotalAmountNeeded { get; set; }
        public long CollectedAmount { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public int Active { get; set; }

        public virtual ICollection<SdbPayments> SdbPayments { get; set; }
        public virtual ICollection<SdbTransaction> SdbTransaction { get; set; }

        public int? sdbSystemUsersId { get; set; }
        [ForeignKey("sdbSystemUsersId")]
        public virtual SdbSystemUsers sdbSystemUsers { get; set; }

        [NotMappedAttribute]
        [BindProperty]
        [Display(Name = "Campaign Image")]
        public IFormFile CampaignImage { get; set; }
       
        //[NotMappedAttribute]
        //public SelectList CampaignList { get; set; }
    }
}
