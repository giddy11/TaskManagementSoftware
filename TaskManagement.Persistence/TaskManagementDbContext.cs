using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.UserManagement;

namespace TaskManagement.Persistence
{
    public class TaskManagementDbContext : DbContext
    {
        public TaskManagementDbContext(DbContextOptions<TaskManagementDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    // Add your model configurations here
        //    modelBuilder.Entity<User>(entity =>
        //    {
        //        entity.HasKey(u => u.Id);
        //        // Add other configurations as needed
        //    });
        //}

        public DbSet<User> Users { get; set; }
    }
}
