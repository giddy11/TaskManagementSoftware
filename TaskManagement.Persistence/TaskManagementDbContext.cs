using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Projects;
using TaskManagement.Domain.TodoTasks;
using TaskManagement.Domain.UserManagement;

namespace TaskManagement.Persistence
{
    public class TaskManagementDbContext : DbContext
    {
        public TaskManagementDbContext(DbContextOptions<TaskManagementDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Project → TodoTasks
            //modelBuilder.Entity<TodoTask>()
            //    .HasOne(t => t.Project)
            //    .WithMany(p => p.TodoTasks)
            //    .HasForeignKey(t => t.ProjectId)
            //    .OnDelete(DeleteBehavior.NoAction); // <---- Breaks cascade loop

            //// Project → CreatedBy (User)
            //modelBuilder.Entity<Project>()
            //    .HasOne(p => p.CreatedBy)
            //    .WithMany()
            //    .HasForeignKey(p => p.CreatedById)
            //    .OnDelete(DeleteBehavior.Restrict);

            //// TodoTask → CreatedBy (User)
            //modelBuilder.Entity<TodoTask>()
            //    .HasOne(t => t.CreatedBy)
            //    .WithMany()
            //    .HasForeignKey(t => t.CreatedById)
            //    .OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<TodoTask> TodoTasks { get; set; }
    }
}
