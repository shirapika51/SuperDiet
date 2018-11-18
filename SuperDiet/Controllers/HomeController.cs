using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperDiet.Models;

namespace SuperDiet.Controllers
{
    public class HomeController : Controller
    {
        private SuperDietContext db;

        public HomeController(SuperDietContext _db)
        {
            db = _db;
        }

        public IActionResult Index(string massage)
        {
            ViewBag.Massage = massage;
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("ID,UserName,Password,IsAdmin,FirstName,LastName")] User user)
        {
            if (ModelState.IsValid)
            {
                db.User.Add(user);
                await db.SaveChangesAsync();
                var userOrder = new Order { ID = user.ID, Date = DateTime.Now };
                db.Order.Add(userOrder);
                await db.SaveChangesAsync();
                if (user.IsAdmin)
                    return RedirectToAction("Index", "AdminPanel");
                else
                    return RedirectToAction("Index", "Items", new { id = user.ID });
            }
            return View(user);
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var loguser = db.User.Where(u => u.UserName == username && u.Password == password).FirstOrDefault();
            if (loguser != null)
            {
                if (loguser.IsAdmin)
                    return RedirectToAction("Index", "AdminPanel");
                else
                    return RedirectToAction("Index", "Items", new { id = loguser.ID });
            }
            ViewBag.Message = "Username or Password incorrect";
            return View();
        }

        public IActionResult Contact()
        {
            var branch = db.Branch.GroupBy(s => s.City).SelectMany(c => c).ToList();

            //var branch = from snifim in db.Branch
            //             group snifim by snifim.City into g
            //             select g;
            return View(branch);
        }

        [HttpGet("Home/getAllBranches")]
        public async Task<IActionResult> GetAllBranches()
        {
            var branch = await db.Branch.ToListAsync();
            return Ok(branch);
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
