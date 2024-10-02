using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCTest.ContectClass;
using MVCTest.Models;
using System.Diagnostics;

namespace MVCTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ConText context;

        public HomeController(ILogger<HomeController> logger, ConText context)
        {
            _logger = logger;
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            var users = await context.UserTable.ToListAsync();
            return View(users);
        }

        [HttpGet]
        public IActionResult Create()
        {
            Users usr = new Users();
            return PartialView("usersModalPartial", usr);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Users usr)
        {
            context.UserTable.Add(usr);
            context.SaveChanges();
            return PartialView("usersModalPartial", usr);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
