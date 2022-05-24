using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sdb.constants;
using sdb.Models;
using sdb.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace sdb.Areas.Administrator.Controllers
{
    [Area("Admin")]
    public class AdminCampaignController : Controller
    {
        private readonly ISDBRepository _sdbRepository;
        public AdminCampaignController(ISDBRepository sdbRepository)
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

        [HttpPost]
        public IActionResult UpdateCampaignStatus(string Id)
        {
            int campID = Convert.ToInt32(Id);
            SdbCompaigns sdbCamp = _sdbRepository.GetCampaignByID(campID);
            sdbCamp.Status = SDB_Constants.CAMPAIGN_DELIVERED_STATUS;
            sdbCamp = _sdbRepository.UpdateCampaign(sdbCamp);
            return Json(new { message = "Updated" });

        }
    }
}
