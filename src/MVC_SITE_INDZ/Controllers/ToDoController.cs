using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_SITE_INDZ.Data;
using MVC_SITE_INDZ.Models;
namespace MVC_SITE_INDZ.Controllers
{
    public class ToDoController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public ToDoController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddTaskViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var username = HttpContext.Session.GetString("Username");

                if (string.IsNullOrEmpty(username))
                {
                    ModelState.AddModelError("", "Користувач не авторизований.");
                    return View(viewModel);
                }

                var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);

                if (user == null)
                {
                    ModelState.AddModelError("", "Користувач не знайдений.");
                    return View(viewModel);
                }

                var task = new ToDoTask
                {
                    Title = viewModel.Title,
                    Description = viewModel.Description,
                    Deadline = viewModel.Deadline,
                    IsCompleted = false,
                    UserId = user.Id 
                };

                await dbContext.Tasks.AddAsync(task);
                await dbContext.SaveChangesAsync();

                return RedirectToAction("List");
            }

            return View(viewModel);
        }


        [HttpGet]
        public async Task<IActionResult> List()
        {
            var tasks = await dbContext.Tasks.ToListAsync();
            return View(tasks); 
        }
    }
}
