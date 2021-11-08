using static System.Console;
using System.Threading;
using System.Threading.Tasks;

namespace Waiting_fot_Time_to_Pass
{
    class Program
    {
        static void Main()
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;

            var t = new Task(() =>
            {
                WriteLine("Press any key to disarm; You have 5 seconds.");
                bool canceled = token.WaitHandle.WaitOne(5000);
                WriteLine(canceled ? "Bomb disarmed" : "BOOM!.");
            },token);

            t.Start();

            ReadKey();
            cts.Cancel();

            WriteLine("Main program done.");
            ReadKey();
        }
    }
}
