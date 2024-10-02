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

        [HttpGet("edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var user =  await context.UserTable.FirstOrDefaultAsync(x=> x.id == id);
            return PartialView("usersModalPartial", user);
        }

        [HttpPost("edit")]
        public async Task<IActionResult> Edit(Users user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await context.UserTable.FirstOrDefaultAsync(x => x.id == user.id);
                if (existingUser != null)
                {
                    existingUser.username = user.username;
                    existingUser.phone = user.phone;
                    existingUser.email = user.email;
                    await context.SaveChangesAsync();
                }
                return RedirectToAction("Index");
            }
            return PartialView("usersModalPartial", user);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var userToDelete = await context.UserTable.FirstOrDefaultAsync(x=> x.id == id);
            if (userToDelete != null)
            {
                context.UserTable.Remove(userToDelete);
                await context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
        
    }
}
