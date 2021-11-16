using System;
using System.Linq;
using System.Threading.Tasks;

namespace As_Parallel_As_parallel_Query
{
    class Program
    {
        static void Main()
        {
            const int count = 50;

            var items = Enumerable.Range(1, count).ToArray();
            var results = new int[count];

            items.AsParallel().ForAll(x =>
            {
                int newValue = x * x * x;
                Console.WriteLine($"{x}--->{newValue} (Task:{Task.CurrentId})");
                results[x - 1] = newValue;
            });
            Console.WriteLine();

            Console.WriteLine("but we put result in the correct cells");
            foreach (var i in results)
            {
                Console.Write($"{i}\t");
            }

            Console.WriteLine("\n\n");
            Console.WriteLine("we can process parallel linq in order:");
            var cubes = items.AsParallel().AsOrdered().Select(x => x * x * x);
            foreach (var i in cubes)
            {
                Console.Write($"{i}\t");
            }

            Console.WriteLine("\n\n");
            Console.WriteLine("See again unordered:");

            var cubesUnordered = items.AsParallel().Select(x => x * x * x);
            foreach (var i in cubesUnordered)
            {
                Console.Write($"{i}\t");
            }


        }
    }
}
