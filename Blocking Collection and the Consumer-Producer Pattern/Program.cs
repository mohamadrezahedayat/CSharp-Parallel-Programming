using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace Blocking_Collection_and_the_Consumer_Producer_Pattern
{
    class Program
    {
        private static readonly BlockingCollection<int> Messages =
            new BlockingCollection<int>(new ConcurrentBag<int>(), 10);

        private static readonly CancellationTokenSource Cts =
            new CancellationTokenSource();

        private static readonly Random Random = new Random();

        static void ProduceAndConsume()
        {
            var producer = Task.Factory.StartNew(RunProducer);
            var consumer = Task.Factory.StartNew(RunConsumer);

            try
            {
                Task.WaitAll(new[] {producer, consumer}, Cts.Token);
            }
            catch (AggregateException ae)
            {
              ae.Handle(e=>true);
            }
        }

        static void Main()
        {
            Task.Factory.StartNew(ProduceAndConsume,Cts.Token);

            ReadKey();
            Cts.Cancel();
        }

        private static void RunConsumer()
        {
            foreach (var item in Messages.GetConsumingEnumerable())
            {
               Cts.Token.ThrowIfCancellationRequested();
               WriteLine($"-{item}\t");
               Thread.Sleep(Random.Next(1000));
            }
        }

        private static void RunProducer()
        {
            while (true)
            {
                Cts.Token.ThrowIfCancellationRequested();
                int i = Random.Next(100);
                Messages.Add(i);

                WriteLine($" +{i}\t");
                Thread.Sleep(Random.Next(100));

            }
            // ReSharper disable once FunctionNeverReturns
        }
    }
}
