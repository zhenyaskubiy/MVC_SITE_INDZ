using Microsoft.AspNetCore.Mvc;
using MVC_SITE_INDZ.Data;
using MVC_SITE_INDZ.Models;
using System.Linq;

namespace MVC_SITE_INDZ.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user != null)
            {
                HttpContext.Session.SetString("Username", user.Username);
                return RedirectToAction("Add", "ToDo");
            }

            ViewBag.ErrorMessage = "Невірний логін або пароль.";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Username");
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = _context.Users.FirstOrDefault(u => u.Username == user.Username);
                if (existingUser == null)
                {
                    _context.Users.Add(user);
                    _context.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Користувач з таким ім'ям вже існує.");
            }
            return View(user);
        }
    }
}
