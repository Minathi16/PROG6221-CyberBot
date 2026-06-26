using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace PROG6221
{
    public class DbTask
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool IsComplete { get; set; } = false;
    }

    public class AppDbContext : DbContext
    {
        public DbSet<DbTask> Tasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Creates a local SQLite database file in your debug folder
            optionsBuilder.UseSqlite("Data Source=database.db");
        }

        public AppDbContext()
        {
            Database.EnsureCreated(); // Auto-creates the database if it doesn't exist
        }
    }
}