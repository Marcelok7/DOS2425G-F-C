
using Microsoft.EntityFrameworkCore;
using TMS.Models;

namespace TMS.Data
{
    public class BackEndContext : DbContext
    {
        public BackEndContext(DbContextOptions<BackEndContext> options)
            : base(options)
        {
        }

        // Defina DbSets para suas entidades
        public DbSet<User> Users { get; set; }
        public DbSet<TaskItem> TaskItems { get; set; }
        public DbSet<ProjectClass> Projects { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurações adicionais do modelo
            //modelBuilder.Entity<User>().ToTable("Users");
            //modelBuilder.Entity<TaskItem>().ToTable("TaskItems");
            //modelBuilder.Entity<ProjectClass>().ToTable("Projects");
            //modelBuilder.Entity<Comment>().ToTable("Comments");
        }
    }

}