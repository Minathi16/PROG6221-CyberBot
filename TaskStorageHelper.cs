using System.Collections.Generic;
using System.Linq;
namespace PROG6221
{
    public class TaskStorageHelper
    {
        public void AddTask(string title)
        {
            using var db = new AppDbContext();
            db.Tasks.Add(new DbTask { Title = title });
            db.SaveChanges();
        }
        public List<DbTask> GetTasks() { using var db = new AppDbContext(); return db.Tasks.ToList(); }
    }
}