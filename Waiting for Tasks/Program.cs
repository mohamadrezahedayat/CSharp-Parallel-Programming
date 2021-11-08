using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace Waiting_for_Tasks
{
    class Program
    {
        static void Main()
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;
            var t1 = new Task(() =>
            {
                WriteLine("I take 5 seconds");
                for (int i = 0; i < 5; i++)
                {
                    token.ThrowIfCancellationRequested();
                    Thread.Sleep(1000);
                }
                WriteLine($"Task {Task.CurrentId} : I'm done.");
            }, token);
            t1.Start();

            Task t2 = Task.Factory.StartNew(() => {
                WriteLine("I take 3 seconds");
                for (int i = 0; i < 3; i++)
                {
                    token.ThrowIfCancellationRequested();
                    Thread.Sleep(1000);
                }
                WriteLine($"Task {Task.CurrentId} : I'm done.");
            }, token);
            //t.Wait(token);

            //Task.WaitAll(t1, t2);
            //Task.WaitAny(t1, t2);
            //Task.WaitAny(new[] {t1, t2},4000);
            //Task.WaitAll(new[] {t1, t2},4000);

            //ReadKey();
            //cts.Cancel();
            Task.WaitAll(new[] {t1, t2},4000,token);
            
            WriteLine($"Task {t1.Id} status is {t1.Status}");
            WriteLine($"Task {t2.Id} status is {t2.Status}");

            WriteLine("Main program done.");
            ReadKey();
        }
    }
}