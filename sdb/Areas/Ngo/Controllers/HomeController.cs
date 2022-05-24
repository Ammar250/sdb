using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sdb.Models;
using sdb.Repository;
using sdb.Data;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using sdb.constants;

namespace sdb.Areas.Ngo.Controllers
{
    [Area("Ngo")]
    public class HomeController : Controller
    {

        private readonly ISDBRepository _sdbRepository;
        public HomeController(ISDBRepository sdbRepository)
        {
            _sdbRepository = sdbRepository;
        }


        public IActionResult Index()
        {
            var loggedInUser = HttpContext.Session.GetString("loggedInUser");
            if (loggedInUser == null)
            {
                return RedirectToAction("Index", "Home"); // User does not logged in Please login
            }
            SdbSystemUsers sdbSystemUsers = new SdbSystemUsers();
            List<SdbCompaigns> campaignInfoWithUser = new List<SdbCompaigns>();

            loggedInUser = HttpContext.Session.GetString("loggedInUser");
            if (loggedInUser != null)
            {
                sdbSystemUsers = JsonSerializer.Deserialize<SdbSystemUsers>(loggedInUser);
                if (sdbSystemUsers.UserRole.Equals(SDB_Constants.USER_ROLE_NGO))
                {
                    campaignInfoWithUser = _sdbRepository.GetAllCampaignsByNGOId(sdbSystemUsers.Id);
                }
                else
                {
                    campaignInfoWithUser = _sdbRepository.GetAllCampaigns();
                }
            }
            return View(campaignInfoWithUser);
        }

        //[HttpGet]
        //public IActionResult AddOrEdit(int id =0)
        //{

        //    return View(new SdbSystemUsers());

        //}
        //[HttpPost]
        //public IActionResult AddorEdit ()
        //{
        //    return View();
        //}
    }
        
}
