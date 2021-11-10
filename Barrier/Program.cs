using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace Barrier_
{
    class Program
    {
        private static readonly Barrier barrier 
            = new Barrier(2, (b) =>
        {
            Console.WriteLine($"Phase {b.CurrentPhaseNumber} is finished");
            
        });

        public static void Water()
        {
            Console.WriteLine("Putting the kettle on ( takes a bit longer)");
            Thread.Sleep(2000);
            barrier.SignalAndWait();
            Console.WriteLine("Pouring Water in the cup");
            barrier.SignalAndWait();
            Console.WriteLine("Putting the kettle away");

        }

        public static void Cup()
        {
            Console.WriteLine("Finding the nicest cup of tea (fast)");
            barrier.SignalAndWait();
            Console.WriteLine("Adding tea.");
            barrier.SignalAndWait();
            Console.WriteLine("Adding sugar.");
        }

        static void Main(string[] args)
        {
            var water = Task.Factory.StartNew(Water);
            var cup = Task.Factory.StartNew(Cup);

            var tea = Task.Factory.ContinueWhenAll(new[] {water, cup}, tasks =>
            {
                Console.WriteLine("Enjoy your cup of tea");
            });
            tea.Wait();
        }
    }
}
