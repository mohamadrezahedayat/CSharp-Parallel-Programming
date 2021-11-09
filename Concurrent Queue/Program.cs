using System.Collections.Concurrent;
using static System.Console;

namespace Concurrent_Queue
{
    internal class Program
    {
        private static void Main()
        {
            var q = new ConcurrentQueue<int>();
            q.Enqueue(1);
            q.Enqueue(2);
            q.Enqueue(3);
            q.Enqueue(4);

            // 4 3 2 1 <- Front

            //       -------------------------------
            //      >>--first_in---->>>--first_out-->>
            //       -------------------------------

            int result;
            if (q.TryDequeue(out result))
            {
                WriteLine($"Removed Element {result}");
            }

            if(q.TryPeek(out result))
            {
                WriteLine($"Front Element is {result}");
            }
        }
    }
}