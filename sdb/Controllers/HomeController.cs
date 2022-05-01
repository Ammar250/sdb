using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using sdb.Models;
using sdb.Repository;
using System.Diagnostics;
using System.Text.Json;

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
            sdbSystemUsers = _sdbRepository.GetSdbSystemUser("abc@abc.com", "Password@12");
            HttpContext.Session.SetString("loggedInUser",JsonSerializer.Serialize(sdbSystemUsers));
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
