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
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Habit> Habits { get; set; }
        public DbSet<Daily> Dailies { get; set; }
        public DbSet<HabbitStats> HabbitStats { get; set; }
        public DbSet<TagsTasks> TagsTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TagsTasks>().HasKey(tt => new { tt.TagId, tt.TaskId });
            modelBuilder.Entity<TagsTasks>().HasOne(tt => tt.Tag).WithMany(tt => tt.TagsTasks).HasForeignKey(tt => tt.TagId);
            modelBuilder.Entity<TagsTasks>().HasOne(tt => tt.Task).WithMany(tt => tt.TagsTasks).HasForeignKey(tt => tt.TaskId);

            modelBuilder.Entity<ToDoTask>().HasData(
                new ToDoTask() { Id = 1, Title = "Action", Content = "Test", MoneyImpact = 5 },
                new ToDoTask() { Id = 3, Title = "Action", Content = "Test", MoneyImpact = 5 }
            );
            modelBuilder.Entity<Subtask>().HasData(
                new Subtask() { Id = 1, TaskId = 1, Name = "First thing to do", Description = "I need to do the first thing", Done = false },
                new Subtask() { Id = 2, TaskId = 1, Name = "Second thing to do", Description = "I need to do the second thing", Done = true },
                new Subtask() { Id = 3, TaskId = 1, Name = "Second thing to do", Description = "I need to do the second thing", Done = true }
            );
            modelBuilder.Entity<Tag>().HasData(
                new Tag() { Id = 1, Name = "Work" }
            );
            modelBuilder.Entity<Habit>().HasData(
                new Habit() { Id = 1, Description = "Drink water", Name = "Water", LifeImpact = 5, NegativeValue = 3, PositiveValue = 4 }
            );
            modelBuilder.Entity<Daily>().HasData(
                new Daily() { Id = 1, Description = "Breakfast", Name = "Breakfast", Created = DateTime.Now }
            );
            modelBuilder.Entity<HabbitStats>().HasData(
                new HabbitStats() { Id = 1, HabitId = 1, NegativeValue = 5, PositiveValue = 1, DateOfUpdate = DateTime.Now }
            );
        }

    }
}
