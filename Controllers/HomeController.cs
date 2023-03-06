using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MvcSistemaSalao.Data;
using MvcSistemaSalao.Models;
using System.Diagnostics;
using System.Web;


namespace MvcSistemaSalao.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MvcSistemaSalaoContext _context;

        public HomeController(ILogger<HomeController> logger, MvcSistemaSalaoContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var clients = from m in _context.Client
                          select m;

           
                clients = clients.Where(x => x.BirthDate.Day == DateTime.Now.Day && x.BirthDate.Month == DateTime.Now.Month);

            if (clients != null)
            {
                var ClientVM = new ClientViewModel
                {
                    Clients = await clients.ToListAsync()
                };

                return View(ClientVM);
            }

            return View();
        }

       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}