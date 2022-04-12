using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sdb.Models;
using sdb.Repository;
using sdb.Data;

namespace sdb.Areas.Ngo.Controllers
{
    [Area("Ngo")]
    public class HomeController : Controller
    {

        private readonly SdbDBContext _db;
        public HomeController(SdbDBContext db)
        {
            _db = db;
        }


        public IActionResult Index()
        {
            var displaydata = _db.SdbSystemUsers.ToList();
            return View(displaydata);
        }

        [HttpGet]
        public IActionResult AddOrEdit(int id =0)
        {

            return View(new SdbSystemUsers());

        }
        [HttpPost]
        public IActionResult AddorEdit ()
        {
            return View();
        }
    }
        
}
