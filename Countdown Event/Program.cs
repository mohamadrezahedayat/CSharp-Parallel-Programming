using System;
using System.Threading;
using System.Threading.Tasks;

namespace Countdown_Event
{
    class Program
    {
        private static int  taskCount=5;
        private static CountdownEvent cte = new CountdownEvent(taskCount);
        private static Random random = new Random();

        static void Main()
        {
            for (int i = 0; i < taskCount; i++)
            {
                Task.Factory.StartNew(() =>
                {
                    Console.WriteLine($"Entering task: {Task.CurrentId}");
                    Thread.Sleep(random.Next(3000));
                    cte.Signal();
                    Console.WriteLine($"Exiting task{Task.CurrentId}");

                });

            }

            var finalTask = Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"waiting for other tasks to complete in {Task.CurrentId}");
                cte.Wait();
                Console.WriteLine("All tasks completed");
            });
            finalTask.Wait();
        }
    }
}
