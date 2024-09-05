using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    public class LoginController : Controller
    {
        private readonly IMSDbContext iMSDbContext;

        public LoginController(IMSDbContext iMSDbContext) {
            this.iMSDbContext = iMSDbContext;
        }

        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Login(User user)
        {
            var Myuser = iMSDbContext.Users.Where(x => x.UserId == user.UserId && x.Password == user.Password).FirstOrDefault();
            if (Myuser != null && Myuser.Status == "Active")
            {
                
                HttpContext.Session.SetString("UserSession", "Welcome "+Myuser.Name);
                return RedirectToAction("Index","Home");

            }
            else
            {
                ViewBag.Message = "Login Failed. Please try again..";
            }
            return View();
        }

        public IActionResult Logout()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {

                HttpContext.Session.Remove("UserSession");
                return RedirectToAction("Login","Login");
            }
            return View();
        }
    }
}
