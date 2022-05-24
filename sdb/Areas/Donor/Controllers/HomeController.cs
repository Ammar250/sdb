using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sdb.Areas.Donor.Controllers
{
    [Area("Donor")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var loggedInUser = HttpContext.Session.GetString("loggedInUser");
            if (loggedInUser == null)
            {
                return RedirectToAction("Index","Home"); // User does not logged in Please login
            }
            return View();
        }
    }
}
