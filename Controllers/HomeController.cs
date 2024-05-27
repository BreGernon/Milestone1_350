using Microsoft.AspNetCore.Mvc;
using Milestone1_350.Models;
using System.Diagnostics;

namespace Milestone1_350.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public static BoardModel gameBoard;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            gameBoard = new BoardModel(1, 8);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
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