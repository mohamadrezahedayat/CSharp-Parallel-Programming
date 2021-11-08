using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace Cancelling_Tasks
{
    class Program
    {
        static void Main()
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;

            token.Register(() =>
            {
                WriteLine("cancellation has been requested");
            });

            var t = new Task(() =>
            {
                var i = 0;
                while (true)
                {
                    //if (token.IsCancellationRequested)
                    //{
                    //    throw new OperationCanceledException();
                    //}
                    token.ThrowIfCancellationRequested();
                    WriteLine($"{i++}\t");
                }
            },token);
            t.Start();

            Task.Factory.StartNew(() =>
            {
                token.WaitHandle.WaitOne();
                WriteLine("Wait handle released, Cancellation was requested");
            });

            ReadKey();
            cts.Cancel();

            WriteLine("Main program done");
            ReadKey();
        }
    }
}
