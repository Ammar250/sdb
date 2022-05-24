using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sdb.Models;
using sdb.Data;
using sdb.constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using sdb.Repository;

namespace sdb.Areas.Ngo.Controllers
{
   
    public class CampaignController : Controller
    {
        private readonly ISDBRepository _sdbRepository;
        private readonly IWebHostEnvironment webHostEnvironment;
        public CampaignController(ISDBRepository sdbRepository, IWebHostEnvironment webHostEnvironment)
        {
            _sdbRepository = sdbRepository;
            this.webHostEnvironment = webHostEnvironment;
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
            
            return View(campaignInfoWithUser) ;   
        }
        [HttpGet]
        public IActionResult GetAllNGOCampaigns()
        {
            SdbSystemUsers sdbSystemUsers = new SdbSystemUsers();
            List<SdbCompaigns> campaignInfoWithUser = new List<SdbCompaigns>();

            var loggedInUser = HttpContext.Session.GetString("loggedInUser");
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
            return PartialView(campaignInfoWithUser);
            
        }
        
        public ActionResult Create()
        {
            return PartialView(new SdbCompaigns());
        }

        [HttpPost]
        public ActionResult Create(SdbCompaigns sdbCampaigns)
        {
            if (ModelState.IsValid)
            {
                if (sdbCampaigns.CampaignImage != null)
                {
                    // If a new photo is uploaded, the existing photo must be
                    // deleted. So check if there is an existing photo and delete
                    if (sdbCampaigns.Image != null)
                    {
                        string filePath = Path.Combine(webHostEnvironment.WebRootPath,
                                "images", sdbCampaigns.Image);
                        System.IO.File.Delete(filePath);
                    }
                    // Save the new photo in wwwroot/images folder and update
                    // Image property of the campaign object
                    sdbCampaigns.Image = ProcessUploadedFile(sdbCampaigns.CampaignImage);
                }

                SdbSystemUsers sdbSystemUsers = new SdbSystemUsers();
                var userInfo = HttpContext.Session.GetString("loggedInUser");

                if (userInfo != null)
                {
                    sdbSystemUsers = JsonSerializer.Deserialize<SdbSystemUsers>(userInfo);

                    sdbCampaigns.CompaignPurpose = SDB_Constants.CAMPAIGN_PURPOSE;
                    sdbCampaigns.Status = SDB_Constants.CAMPAIGN_START_STATUS;
                    sdbCampaigns.sdbSystemUsersId = sdbSystemUsers.Id;
                    sdbCampaigns.sdbSystemUsers = null;
                    sdbCampaigns.Active = 1;
                    sdbCampaigns.CreatedAt = DateTime.Now;
                    sdbCampaigns.CreatedBy = sdbSystemUsers.Name;
                    sdbCampaigns.UpdatedAt = DateTime.Now;
                    sdbCampaigns.UpdatedBy = sdbSystemUsers.Name;
                    _sdbRepository.AddNewCampaign(sdbCampaigns);
                }
                return Json(new { formStatus = "Success" });
            }
            return PartialView(sdbCampaigns);
        }
        [HttpGet]
        public IActionResult EditCampaign(string Id)
        {
            int campID = Convert.ToInt32(Id);
            var sdbCamp = _sdbRepository.GetCampaignByID(campID);


            return PartialView(sdbCamp);  

        }
        [HttpPost]
        public IActionResult EditCampaign(SdbCompaigns sdbCampaign)
        {
            if (sdbCampaign.CampaignImage != null)
            {
                // If a new photo is uploaded, the existing photo must be
                // deleted. So check if there is an existing photo and delete
                if (sdbCampaign.Image != null)
                {
                    string filePath = Path.Combine(webHostEnvironment.WebRootPath,
                            "images", sdbCampaign.Image);
                    System.IO.File.Delete(filePath);
                }
                // Save the new photo in wwwroot/images folder and update
                // Image property of the campaign object
                sdbCampaign.Image = ProcessUploadedFile(sdbCampaign.CampaignImage);
            }
            SdbSystemUsers sdbSystemUsers = new SdbSystemUsers();
            var userInfo = HttpContext.Session.GetString("loggedInUser");
            sdbSystemUsers = JsonSerializer.Deserialize<SdbSystemUsers>(userInfo);

            sdbCampaign.CompaignPurpose = SDB_Constants.CAMPAIGN_PURPOSE;
            sdbCampaign.Status = SDB_Constants.CAMPAIGN_START_STATUS;
            sdbCampaign.sdbSystemUsersId = sdbSystemUsers.Id;
            sdbCampaign.sdbSystemUsers = null;
            sdbCampaign.Active = 1;
            sdbCampaign.CreatedAt = DateTime.Now;
            sdbCampaign.CreatedBy = sdbSystemUsers.Name;
            sdbCampaign.UpdatedAt = DateTime.Now;
            sdbCampaign.UpdatedBy = sdbSystemUsers.Name;
            _sdbRepository.UpdateCampaign(sdbCampaign);
            
            return RedirectToAction("Index", "Campaign");
        }
        [HttpGet]
        public IActionResult DeleteCampaign(string Id)
        {
            int campID = Convert.ToInt32(Id);
            var sdbCampaign = _sdbRepository.GetCampaignByID(campID);
            return View(sdbCampaign);
           

        }
        [HttpPost]
        public IActionResult Deletecampaign(int id)
        {
            _sdbRepository.DeleteCampaign(id);
           
            return RedirectToAction("Index", "Campaign");
        }

        [HttpGet]
        public IActionResult CloseCampaign(string Id)
        {
            int campID = Convert.ToInt32(Id);
            SdbCompaigns sdbCamp = _sdbRepository.GetCampaignByID(campID);
            sdbCamp.Status = SDB_Constants.CAMPAIGN_CLOSE_STATUS;
            sdbCamp = _sdbRepository.UpdateCampaign(sdbCamp);
            return RedirectToAction("Index", "Campaign");

        }

        // This method is responsible to upload image in wwwroot/images/gallery directory
        // and generates unique name for image to store into the database
        private string ProcessUploadedFile(IFormFile campaignImage)
        {
            string uniqueFileName = null;

            if (campaignImage != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images/gallery");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + campaignImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    campaignImage.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
        
    }
}
