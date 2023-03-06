using Microsoft.AspNetCore.Mvc;
using MvcSistemaSalao.Data;
using MvcSistemaSalao.Models;

namespace MvcSistemaSalao.Controllers
{
    public class LoginController : Controller
    {
        private readonly MvcSistemaSalaoContext _context;

        public LoginController(MvcSistemaSalaoContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.ShowNav = 0;
            Login _login = new Login();
            return View(_login);
        }

        [HttpPost]
        public IActionResult Index(Login _login)
        {
            var status = _context.Login.Where(m => m.LoginID == _login.LoginID && m.Password == _login.Password).FirstOrDefault();

            if (status == null)
            {
                
                ViewBag.LoginStatus = 1;
                ViewBag.ShowNav = 0;
                return View(_login);
            }
            else
            {
                TempData["username"] = _login.LoginID;
                TempData.Keep("username");
                return RedirectToAction("Index", "Home");
            }

            
        }
    }
}
