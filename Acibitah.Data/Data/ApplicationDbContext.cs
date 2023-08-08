using Acibitah.Models;
using Microsoft.EntityFrameworkCore;

namespace Acibitah.Data.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<ToDoTask> ToDoTasks { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDoTask>().HasData(
                new ToDoTask() { Id = 1, Title = "Action", Content = "Test" }
            );
        }

    }
}
