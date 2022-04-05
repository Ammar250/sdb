using Microsoft.AspNetCore.Mvc;
using sdb.Models;
using sdb.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sdb.Controllers
{
    public class LoginController : Controller
    {
        private readonly ISDBRepository _sdbRepository;

        public LoginController(ISDBRepository sdbRepository)
        {
            _sdbRepository = sdbRepository;
        }
        public IActionResult login()
        {
            return View();
            //How to Encode or Encrept Password in Asp.net core 
        }
        [HttpGet]
        public String Get(string userEmail, string password)
        {
            
            try
            {
                SdbSystemUsers sdbSystemUsers = _sdbRepository.GetSdbSystemUser(userEmail, password);
                if (sdbSystemUsers != null)
                {
                    return "You have successfully Login ";
                }
                return "Error in User Login";
            }
            catch
            {
                return "Error in User Login";
            }
           
        }
    }
}
