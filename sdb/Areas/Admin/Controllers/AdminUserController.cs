using Microsoft.AspNetCore.Hosting;
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

namespace sdb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminUserController : Controller
    {
        private readonly ISDBRepository _sdbRepository;
        public AdminUserController(ISDBRepository sdbRepository)
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
            List<SdbSystemUsers> UserInfo = new List<SdbSystemUsers>();

            loggedInUser = HttpContext.Session.GetString("loggedInUser");
            if (loggedInUser != null)
            {
                sdbSystemUsers = JsonSerializer.Deserialize<SdbSystemUsers>(loggedInUser);
                if (sdbSystemUsers.UserRole.Equals(SDB_Constants.USER_ROLE_ADMIN))
                { 
                    UserInfo = _sdbRepository.GetAllSdbSystemUsers();
                }
            }

            return View(UserInfo);
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
        [HttpGet]
        public ActionResult Create()
        {
            return PartialView(new SdbSystemUsers());
        }
        [HttpPost]
        public ActionResult Create(SdbSystemUsers sdbSystemUsers)
        {
            if (ModelState.IsValid)
            {
                sdbSystemUsers.Active = 1;
                sdbSystemUsers.CreatedAt = DateTime.Now;
                sdbSystemUsers.CreatedBy = sdbSystemUsers.Name;
                sdbSystemUsers.UpdatedAt = DateTime.Now;
                sdbSystemUsers.UpdatedBy = sdbSystemUsers.Name;
                _sdbRepository.AddNewUser(sdbSystemUsers);
                return Json(new { formStatus = "Success" });
            }

            return PartialView(sdbSystemUsers);
        }
        [HttpGet]
        public IActionResult EditUser(string Id)
        {
            int userID = Convert.ToInt32(Id);
            var sdbCamp = _sdbRepository.GetSdbSystemUsersByID(userID);


            return PartialView(sdbCamp);

        }
        [HttpPost]
        public IActionResult EditUser(SdbSystemUsers sdbSystemUsers)
        {
            
            
            var userInfo = HttpContext.Session.GetString("loggedInUser");
            sdbSystemUsers.Active = 1;
            sdbSystemUsers.CreatedAt = DateTime.Now;
            sdbSystemUsers.CreatedBy = sdbSystemUsers.Name;
            sdbSystemUsers.UpdatedAt = DateTime.Now;
            sdbSystemUsers.UpdatedBy = sdbSystemUsers.Name;
            _sdbRepository.UpdateUser(sdbSystemUsers);
            

            return RedirectToAction("Index", "AdminUser");
        }
        [HttpGet]
        public IActionResult DeleteUser(string Id)
        {
            int campID = Convert.ToInt32(Id);
            var sdbUser = _sdbRepository.GetSdbSystemUsersByID(campID);
            return View(sdbUser);


        }
        [HttpPost]
        public IActionResult DeleteUser(int id)
        {
            _sdbRepository.DeleteUser(id);

            return RedirectToAction("Index", "Campaign");
        }

        
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var loggedInUser = HttpContext.Session.GetString("loggedInUser");
            if (loggedInUser == null)
            {
                return RedirectToAction("Index", "Home"); // User does not logged in Please login
            }
            SdbSystemUsers sdbSystemUsers = new SdbSystemUsers();
            List<SdbSystemUsers> UserInfo = new List<SdbSystemUsers>();

            loggedInUser = HttpContext.Session.GetString("loggedInUser");
            if (loggedInUser != null)
            {
                sdbSystemUsers = JsonSerializer.Deserialize<SdbSystemUsers>(loggedInUser);
                if (sdbSystemUsers.UserRole.Equals(SDB_Constants.USER_ROLE_ADMIN))
                {
                    UserInfo = _sdbRepository.GetAllSdbSystemUsers();
                }
            }
            return PartialView(UserInfo);

        }
    }
 }
