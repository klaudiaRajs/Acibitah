using Acibitah.Models;
using Microsoft.EntityFrameworkCore;

namespace Acibitah.Data.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public ApplicationDbContext()
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            this.ChangeTracker.LazyLoadingEnabled = false;
        }
        public DbSet<ToDoTask> ToDoTasks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Hero> Heroes { get; set; }
        public DbSet<Subtask> Subtasks { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDoTask>().HasData(
                new ToDoTask() { Id = 1, Title = "Action", Content = "Test" }
            );
            modelBuilder.Entity<Subtask>().HasData(
                new Subtask() { Id = 1, TaskId = 1, Name = "First thing to do", Description = "I need to do the first thing", Done = false },
                new Subtask() { Id = 2, TaskId = 1, Name = "Second thing to do", Description = "I need to do the second thing", Done = true }
            );
        }

    }
}
