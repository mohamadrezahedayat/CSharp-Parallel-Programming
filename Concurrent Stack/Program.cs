using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using static System.Console;
using System.Text;
using System.Threading.Tasks;

namespace Concurrent_Stack
{
    class Program
    {
        static void Main(string[] args)
        {
            var stack = new ConcurrentStack<int>();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            stack.Push(4);

            int result;
            if (stack.TryPeek(out result))
            {
                WriteLine($"{result} is on top");
            } 

            WriteLine();
            
            if (stack.TryPop(out result))
            {
                WriteLine($"{result} is on top");
            }

            WriteLine();

            var items = new int[5];
            if (stack.TryPopRange(items, 0, 5) > 0)
            {
                var text = 
                    string.Join(", ", items.Select(i=>i.ToString()));

                WriteLine($"Popped this items: {text}");
            }
        }
    }
}
