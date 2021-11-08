using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace Composite_Cancellation_Tokens
{
    class Program
    {
        static void Main()
        {
            var planned = new CancellationTokenSource();
            var preventative = new CancellationTokenSource();
            var emergency = new CancellationTokenSource();
            var paranoid =
                CancellationTokenSource.CreateLinkedTokenSource(planned.Token, preventative.Token, emergency.Token);

            Task.Factory.StartNew(() =>
            {
                var i = 0;
                while (true)
                {
                    paranoid.Token.ThrowIfCancellationRequested();
                    WriteLine($"{i++}");
                    Thread.Sleep(1000);
                }
            },paranoid.Token);

            ReadKey();
            emergency.Cancel();

            WriteLine("Main program done.");
            ReadKey();
        }
    }
}
