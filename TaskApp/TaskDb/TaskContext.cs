using Microsoft.EntityFrameworkCore;

namespace TaskApp.TaskDb
{
    public class TaskContext: DbContext
    {
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public TaskContext(DbContextOptions<TaskContext> options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasOne(u => u.Organization)
                .WithMany(c => c.Employees)
                .HasForeignKey(u => u.OrganizationId);
        }
    }
}
