using Microsoft.AspNetCore.Mvc;
using Milestone1_350.Models;
using Milestone1_350.Service; 

namespace Milestone1_350.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProcessRegister(UserModel userModel)
        {
            SecurityService securityService = new SecurityService();

            if (securityService.Register(userModel))
                return View("RegisterSuccess", userModel);
            else
                return View("RegisterFail", userModel);
        }
    }
}
