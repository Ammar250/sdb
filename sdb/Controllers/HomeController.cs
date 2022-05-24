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
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
