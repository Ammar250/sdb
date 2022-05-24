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

namespace sdb.Areas.Donor.Controllers
{
    [Area("Donor")]
    public class TrackDonationController : Controller
    {
        private readonly ISDBRepository _sdbRepository;
        public TrackDonationController(ISDBRepository sdbRepository)
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
            List<SdbTransaction> transactionInfoWithCampaign = new List<SdbTransaction>();

            loggedInUser = HttpContext.Session.GetString("loggedInUser");
            if (loggedInUser != null)
            {
                sdbSystemUsers = JsonSerializer.Deserialize<SdbSystemUsers>(loggedInUser);
                if (sdbSystemUsers.UserRole.Equals(SDB_Constants.USER_ROLE_DONOR))
                {
                    transactionInfoWithCampaign = _sdbRepository.GetAllTransactionsByUserId(sdbSystemUsers.Id);
                }
            }

            return View(transactionInfoWithCampaign);
        }
    }
}
