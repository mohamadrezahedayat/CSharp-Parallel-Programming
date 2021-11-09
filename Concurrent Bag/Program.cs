using System;
using static System.Console;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concurrent_Bag
{
    class Program
    {
        static void Main(string[] args)
        {
            // stack: Last In First Out
            // Queue: First In Last Out
            // Bag: No Ordering

            var bag = new ConcurrentBag<int>();
            var tasks = new List<Task>();

            for (int i = 0; i < 10; i++)
            {
                var i1 = i;
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    bag.Add(i1);

                    WriteLine($"Task({Task.CurrentId}) has added {i1} ");
                    int result;
                    if (bag.TryPeek(out result))
                    {
                        WriteLine($"Task({Task.CurrentId}) has peeked the value {result}");
                    }
                }));
            }

            Task.WaitAll(tasks.ToArray());

            int last;
            if (bag.TryTake(out last))
            {
                WriteLine($"I got {last}");
            }
        }
    }
}
