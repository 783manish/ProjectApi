using Microsoft.EntityFrameworkCore;

namespace SystemProjectApi.Model
{
    public class ProjectDbContext : DbContext
	{
		public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options)
		{
		}

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Project> Projects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Projects)
                .WithMany(p => p.Employees)
                .UsingEntity(j => j.ToTable("EmployeeProject"));

            modelBuilder.Entity<Project>()
                .HasOne(p => p.ProjectManagerName)
                .WithMany()
                .HasForeignKey(p => p.ProjectManagerId)
                .OnDelete(DeleteBehavior.NoAction);
          
            base.OnModelCreating(modelBuilder);
        }
    }
}

