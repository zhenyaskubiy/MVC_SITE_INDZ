using Microsoft.EntityFrameworkCore;
using MVC_SITE_INDZ.Models;

namespace MVC_SITE_INDZ.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<ToDoTask> Tasks { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
