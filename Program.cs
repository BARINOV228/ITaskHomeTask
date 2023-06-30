using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TaskTracker<TaskUnit> tracker = new TaskTracker<TaskUnit>();

            tracker.Add(new TaskUnit { Name = "Task 1", Description = "Description 1", DueDate = new DateTime(2021, 12, 31) });
            tracker.Add(new TaskUnit { Name = "Task 2", Description = "Description 2", DueDate = new DateTime(2021, 12, 10) });
            tracker.Add(new TaskUnit { Name = "Task 3", Description = "Description 3", DueDate = new DateTime(2021, 12, 20) });
            tracker.Add(new TaskUnit { Name = "Task 4", Description = "Description 4", DueDate = new DateTime(2021, 12, 15) });
            tracker.Add(new TaskUnit { Name = "Task 5", Description = "Description 5", DueDate = new DateTime(2021, 12, 25) });

            List<TaskUnit> latestTasks = tracker.LatestTasks();
            TaskUnit nearestTask = latestTasks.FirstOrDefault();

            if (nearestTask != null)
            {
                Console.WriteLine($"Самая близкая по дате задача: {nearestTask.Name}, {nearestTask.DueDate}");
            }
        }



        public interface ITaskUnit
        {
            string Name { get; set; }
            string Description { get; set; }
            DateTime DueDate { get; set; }
        }

        public class TaskUnit : ITaskUnit
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public DateTime DueDate { get; set; }
        }

        public class TaskTracker<T> where T : ITaskUnit
        {
            private List<T> tasks = new List<T>();

            public void Add(T task)
            {
                tasks.Add(task);
            }

            public void Remove(T task)
            {
                tasks.Remove(task);
            }

            public List<T> LatestTasks()
            {
                return tasks.OrderBy(t => t.DueDate).ToList();
            }

            public int TasksNumber()
            {
                return tasks.Count;
            }
        }

        public delegate void Notify(ITaskUnit task);

        public static class TaskTracker
        {
            public static Notify Notifier;

            public static void SendSms(ITaskUnit task)
            {
                Console.WriteLine($"Отправка SMS: {task.Name}");
            }

            public static void SendEmail(ITaskUnit task)
            {
                Console.WriteLine($"Отправка Email: {task.Name}");
            }
        }
    }
}
