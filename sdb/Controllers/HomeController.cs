using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using sdb.Models;
using sdb.Repository;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;
using Stripe;
using System;
using sdb.constants;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace sdb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISDBRepository _sdbRepository;

        public HomeController(ISDBRepository sdbRepository,ILogger<HomeController> logger)
        {
            _sdbRepository = sdbRepository;
            _logger = logger;
        }

        public IActionResult Index()
        {
       
            SdbSystemUsers sdbSystemUsers = new SdbSystemUsers();
            List<SdbCompaigns> campaignInfoWithUser = new List<SdbCompaigns>();
            campaignInfoWithUser = _sdbRepository.GetAllCampaigns();
            ViewBag.CampaignList = new SelectList(campaignInfoWithUser, "Id","Name");
            return View(campaignInfoWithUser);
        }
        [HttpPost]
        public IActionResult Charge(string Token, long Total, string Email, int campaignId)

        { 
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
                    Amount = Total * 100, // we need to multiply user amount because stripe consider amount provided in cents
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

                    
                    
                    SdbSystemUsers sdbSystemUsers = _sdbRepository.GetSdbSystemUser("unknown@gmail.com", "Unknown");
                    
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
                        return Json(new { redirecturl = "/Home/Index", TransactionId = sdbTransaction.Id }); // Redirect to donor index page
                    }

                    return Json(new { redirecturl = "Unable to save transaction in SDB database" }); // Show error at front end


                }
                else
                {
                    return Json(new { redirecturl = "Stripe Error: " + charge.FailureMessage }); // Stripe error handling
                }
            }
            catch (Exception ex)
            {
                return Json(new { redirecturl = ex.Message }); // Stripe exception handling
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
