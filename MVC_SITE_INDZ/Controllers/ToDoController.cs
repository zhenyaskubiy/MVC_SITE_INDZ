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

        // GET: Add Task
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        // POST: Add Task
        [HttpPost]
        public async Task<IActionResult> Add(AddTaskViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var task = new ToDoTask
                {
                    Title = viewModel.Title,
                    Description = viewModel.Description,
                    Deadline = viewModel.Deadline,  // Використовуємо Deadline з ViewModel
                    IsCompleted = false,  // За замовчуванням завдання не завершене
                    AddedByUser = viewModel.AddedBy
                };

                await dbContext.Tasks.AddAsync(task);
                await dbContext.SaveChangesAsync();

                return RedirectToAction("List");
            }

            return View(viewModel);
        }

        // GET: Task List
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var tasks = await dbContext.Tasks.ToListAsync();
            return View(tasks);
        }

        // GET: Edit Task
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var task = await dbContext.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            // Створюємо ViewModel для редагування
            var viewModel = new AddTaskViewModel
            {
                Title = task.Title,
                Description = task.Description,
                Deadline = task.Deadline,
                AddedBy = task.AddedByUser
            };

            return View(viewModel);
        }

        // POST: Edit Task
        [HttpPost]
        public async Task<IActionResult> Edit(AddTaskViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var task = await dbContext.Tasks.FindAsync(viewModel.Id);
                if (task != null)
                {
                    task.Title = viewModel.Title;
                    task.Description = viewModel.Description;
                    task.Deadline = viewModel.Deadline;
                    task.IsCompleted = viewModel.IsCompleted;

                    await dbContext.SaveChangesAsync();
                }

                return RedirectToAction("List");
            }

            return View(viewModel);
        }

        // POST: Mark Task as Completed
        [HttpPost]
        public async Task<IActionResult> MarkComplete(Guid id)
        {
            var task = await dbContext.Tasks.FindAsync(id);
            if (task != null)
            {
                task.IsCompleted = true;
                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List");
        }
    }
}
