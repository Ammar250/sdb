using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sdb.Areas.Ngo.Controllers
{
    public class CampaginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
