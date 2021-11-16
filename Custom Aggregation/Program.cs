using System;
using System.Linq;

namespace Custom_Aggregation
{
    class Program
    {
        static void Main()
        {
            var sum = Enumerable.Range(1, 1000).Sum();
            var sum2 = Enumerable.Range(1, 1000).Aggregate(0,(i,acc)=>i+acc);
            var avg = Enumerable.Range(1, 1000).Average();
            Console.Write(" aggregate sequentially:\t");
            Console.Write($"sum: {sum}\t"); Console.Write($"sum2: {sum2}\t");
            Console.Write($"average: {avg}\t");

            Console.WriteLine("\n");
            var sumParallel = ParallelEnumerable.Range(1, 1000)
                .Aggregate(
                0,
                (partialSum,i)=>partialSum + i,
                (total,subTotal)=> total + subTotal,
                i=>i
                );
            Console.Write("aggregate Parallel:\t");
            Console.Write($"parallel sum: {sumParallel}\t");
            Console.WriteLine();
        }
    }
}
