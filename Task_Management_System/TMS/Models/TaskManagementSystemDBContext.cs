using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace TMS.Models
{
    public class TaskManagementSystemDBContext : DbContext
    {
        public DbSet<Comment> comments { get; set; }
        public DbSet<ProjectClass> projectClasses { get; set; }
        public DbSet<TaskItem> tasks { get; set; }
        public DbSet<User> users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=192.168.1.206;Database=TaskManagementSystem;User ID=sa;Password=RPSsql12345;TrustServerCertificate=True;");

        }
    }
}
