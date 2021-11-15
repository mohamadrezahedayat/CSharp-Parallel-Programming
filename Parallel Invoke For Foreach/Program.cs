using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parallel_Invoke_For_Foreach
{
    class Program
    {
        public static IEnumerable<int> Range (int start,int end, int step)
        {
            for (int i=start ; i < end; i +=step)
            {
                yield return i;
            }
        }
        static void Main()
        {
            Console.WriteLine("Parallel Invoke:\n");
            var a = new Action(()=> Console.WriteLine($"First, task Id:{Task.CurrentId}"));
            var b = new Action(()=> Console.WriteLine($"Second, task Id:{Task.CurrentId}"));
            var c = new Action(()=> Console.WriteLine($"Third, task Id:{Task.CurrentId}"));
            Parallel.Invoke(a,b,c);

            Console.WriteLine("\n\nParallel For:");
            Parallel.For(1, 11, i =>
            {
                Console.WriteLine($"{i}:{i * i}");
            });


            Console.WriteLine("\n\nParallel For (with options):");
            var po = new ParallelOptions();
            //po.CancellationToken
            //po.MaxDegreeOfParallelism
            //po.TaskScheduler
            Parallel.For(1, 11,po, i =>
            {
                Console.WriteLine($"{i}:{i * i}");
            });

            Console.WriteLine("\n\nParallel Foreach:");
            string[] words = {"oh", "what", "a", "night"};
            Parallel.ForEach(words, word =>
            {
                Console.WriteLine($" \"{word}\" ---> has length : {word.Length}, (task {Task.CurrentId}) ");
            });

            Console.WriteLine("\n\nParallel Foreach (custom step):");
            Parallel.ForEach(Range(1, 20, 3), Console.WriteLine);
        }
    }
}
