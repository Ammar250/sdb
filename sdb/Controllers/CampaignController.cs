using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sdb.Models;
using sdb.Data;


namespace sdb.Areas.Ngo.Controllers
{
   
    public class CampaignController : Controller
    {
        private readonly SdbDBContext _db;
        public CampaignController(SdbDBContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            int userId = 1; // TODO: we need to get userid from session
            var displaydata = _db.SdbCompaigns.Where(x => x.UserId == userId).ToList<SdbCompaigns>();
            return View(displaydata);   
        }
        [HttpGet]
        public IActionResult GetAllNGOCampaigns(int userId)
        {
            var displaydata = _db.SdbCompaigns.Where(x => x.UserId == userId).ToList<SdbCompaigns>();
            return PartialView(displaydata);
            
        }
        
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return PartialView(new SdbCompaigns());
            else
            {
                return PartialView(_db.SdbCompaigns.Where(x => x.Id == id).FirstOrDefault<SdbCompaigns>());

            }
        }
        public IActionResult AddCampaign(SdbCompaigns sdbCampaigns)
        {

            sdbCampaigns.CompaignPurpose = "default";
            sdbCampaigns.Status = "Start";
            sdbCampaigns.UserId = 1;
            sdbCampaigns.Active = 1;
            sdbCampaigns.CreatedAt = DateTime.Now;
            sdbCampaigns.CreatedBy = "Test user";
            sdbCampaigns.UpdatedAt = DateTime.Now;
            sdbCampaigns.UpdatedBy = "Test user";
            _db.SdbCompaigns.Add(sdbCampaigns);
            _db.SaveChanges();
            return PartialView("_Campaign",sdbCampaigns);
        }
        public IActionResult EditCampaign(int id=0)
        {
            if (id != 0)
                return PartialView(new SdbCompaigns());
            else
            {
                return PartialView(_db.SdbCompaigns.Where(x => x.Id == id).FirstOrDefault<SdbCompaigns>());

            }
            
        }
        public IActionResult DeleteCampaign(int id=0)
        {
            if (id != 0)
                return PartialView(new SdbCompaigns());
            else
            {
                return PartialView(_db.SdbCompaigns.Where(x => x.Id == id).FirstOrDefault<SdbCompaigns>());

            }
            
        }
    }

    
}
