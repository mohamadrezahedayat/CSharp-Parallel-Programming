using static System.Console;
using System.Threading.Tasks;

namespace Continuations
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("example 1 : Continue a Task by next Task\n");
            var task1 = Task.Factory.StartNew(() => { WriteLine("Boiling Water "); });

            var task2 = task1.ContinueWith(t => WriteLine($"Completed task {t.Id}, pour water into cup"));

            task2.Wait();

            WriteLine("\n\nexample 2: tasks with result (ContinueWhenAll)\n");

            var task3 = Task.Factory.StartNew(() => "Task 1");
            var task4 = Task.Factory.StartNew(() => "Task 2");

            var task5 = Task.Factory.ContinueWhenAll(new[] { task3, task4 }, tasks =>
              {
                  WriteLine("Tasks completed");
                  foreach (var t in tasks)
                  {
                      WriteLine($" - {t.Result}");
                  }
                  WriteLine("All tasks done");
              });
            task5.Wait();

            WriteLine("\n\nexample 3: tasks with result (ContinueWhenAny)\n\n");

            var task6 = Task.Factory.StartNew(() => "Task 1");
            var task7 = Task.Factory.StartNew(() => "Task 2");

            var task8 = Task.Factory.ContinueWhenAny(new[] { task6, task7 }, task =>
            {
                WriteLine("Tasks completed");
                    WriteLine($" - {task.Result}");
                WriteLine("All tasks done");
            });
            task8.Wait();
        }
    }
}
