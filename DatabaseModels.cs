using Microsoft.EntityFrameworkCore;
namespace PROG6221
{
    public class DbTask { public int Id { get; set; } public string Title { get; set; } = ""; public bool IsComplete { get; set; } }
    public class AppDbContext : DbContext
    {
        public DbSet<DbTask> Tasks { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite("Data Source=database.db");
        public AppDbContext() => Database.EnsureCreated();
    }
}