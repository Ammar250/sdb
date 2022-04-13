using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sdb.Models;
using sdb.Data;


namespace sdb.Areas.Ngo.Controllers
{
    [Area("Ngo")]
    public class CampaignController : Controller
    {
        private readonly SdbDBContext _db;
        public CampaignController(SdbDBContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var displaydata = _db.SdbCompaigns.ToList();
            return PartialView(displaydata);
            
        }
    }
}
