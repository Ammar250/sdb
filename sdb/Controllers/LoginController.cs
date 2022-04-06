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
        [HttpPost]
        public String Get(SdbSystemUsers sdbSystemUsers)
        {
            
            try
            {
                sdbSystemUsers = _sdbRepository.GetSdbSystemUser(sdbSystemUsers.Email, sdbSystemUsers.Password);
                if (sdbSystemUsers != null)
                {
                    return sdbSystemUsers.UserRole;
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
