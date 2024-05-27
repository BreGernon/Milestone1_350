using Microsoft.AspNetCore.Mvc;
using Milestone1_350.Models;
using Milestone1_350.Service;

namespace Milestone1_350.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProcessLogin(UserModel user)
        {
            SecurityService securityService = new SecurityService();
            if (securityService.ValidUser(user))
            {
                return View("LoginSuccess", user);
            }
            else
            {
                return View("LoginFailure", user);
            }
        }
    }
}
