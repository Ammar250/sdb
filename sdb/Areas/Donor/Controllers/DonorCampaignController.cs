using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using sdb.Models;
using sdb.constants;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using sdb.Repository;
using Stripe;
using System;
using sdb.Data;

namespace sdb.Areas.Ngo.Controllers
{
    [Area("Donor")]
    public class DonorCampaignController : Controller
    {
        private readonly ISDBRepository _sdbRepository;
        public DonorCampaignController(ISDBRepository sdbRepository)
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
            
            return View(campaignInfoWithUser) ;   
        }

        [HttpPost]
        public IActionResult Charge(string Token,long Total,string Email,int campaignId)
        {
            var loggedInUser = HttpContext.Session.GetString("loggedInUser");
            if (loggedInUser == null)
            {
                return Json(new { redirecturl = "RequiredLogIn" }); // User does not logged in Please login
            }
            var customers = new CustomerService();
            var charges = new ChargeService();

            var customer = customers.Create(new CustomerCreateOptions
            {
                Email = Email,
                Source = Token
            });
            try
            {
                var charge = charges.Create(new ChargeCreateOptions
                {
                    Amount = Total*100, // we need to multiply user amount because stripe consider amount provided in cents
                    Description = "Test Payment",
                    Currency = "pkr",
                    Customer = customer.Id,
                    Metadata = new Dictionary<string, string>()
                    {
                        {"CampaignId",Convert.ToString(campaignId) }
                    }
                });
                if (charge.Status == "succeeded")
                {
                    string BalanceTransactionId = charge.BalanceTransactionId;

                    if (loggedInUser != null)
                    {
                        SdbSystemUsers sdbSystemUsers = JsonSerializer.Deserialize<SdbSystemUsers>(loggedInUser);
                        if (sdbSystemUsers.UserRole.Equals(SDB_Constants.USER_ROLE_DONOR))
                        {
                            // Updated user entered amount in campaign table
                            SdbCompaigns sdbCompaigns = _sdbRepository.GetCampaignByID(campaignId);
                            long oldCollectedAmount = sdbCompaigns.CollectedAmount;

                            sdbCompaigns.Status = SDB_Constants.CAMPAIGN_RUNNING_STATUS;
                            if ((oldCollectedAmount + Total) == sdbCompaigns.TotalAmountNeeded)
                            {
                                sdbCompaigns.Status = SDB_Constants.CAMPAIGN_STOP_STATUS;
                            }
                            sdbCompaigns.CollectedAmount = oldCollectedAmount + Total;
                            sdbCompaigns = _sdbRepository.UpdateCampaign(sdbCompaigns);

                            // Insert into Sdb Transaction table for record
                            SdbTransaction sdbTransaction = new SdbTransaction();
                            sdbTransaction.CompaignId = campaignId;
                            sdbTransaction.DonorId = sdbSystemUsers.Id;
                            sdbTransaction.DonationAmount = Total;
                            
                            sdbTransaction.Active = 1;
                            sdbTransaction.CreatedAt = DateTime.Now;
                            sdbTransaction.CreatedBy = sdbSystemUsers.Name;
                            sdbTransaction.UpdatedAt = DateTime.Now;
                            sdbTransaction.UpdatedBy = sdbSystemUsers.Name;

                            sdbTransaction = _sdbRepository.AddNewTransaction(sdbTransaction); // Save Transaction into SDB database
                            if (sdbTransaction != null)
                            {
                                return Json(new { redirecturl = "/Donor/DonorCampaign/Index", TransactionId = sdbTransaction.Id }); // Redirect to donor index page
                            }

                            return Json(new { redirecturl = "Unable to save transaction in SDB database" }); // Show error at front end
                        }
                        else
                        {
                            return Json(new { redirecturl = "InValidAccess" }); // when someone does not have donor role
                        }
                    }
                    else
                    {   // User info does not exist in session
                        return Json(new { redirecturl = "RequiredLogIn" }); // User does not logged in Please login
                    }
                }
                else
                {
                    return Json(new { redirecturl = "Stripe Error: " + charge.FailureMessage }); // Stripe error handling
                }
            }
            catch(Exception ex)
            {
                return Json(new { redirecturl = ex.Message }); // Stripe exception handling
            }
        }
        [HttpPost]
        public IActionResult DonateToCampaign(string Id)
        {
            int campID = Convert.ToInt32(Id);
            var sdbCamp = _sdbRepository.GetCampaignByID(campID);
            ViewBag.sdbCamp = sdbCamp;
            ViewBag.RequiredAmount = (sdbCamp.TotalAmountNeeded - sdbCamp.CollectedAmount);
            return PartialView();

        }

    }
}
