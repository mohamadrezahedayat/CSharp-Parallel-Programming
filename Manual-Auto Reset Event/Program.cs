using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace Manual_Auto_Reset_Event
{
    class Program
    {
        static void Main(string[] args)
        {
            var mre = new ManualResetEventSlim();
            var are = new AutoResetEvent(false);
            Task.Factory.StartNew(() =>
            {
                WriteLine("Boiling water");
                mre.Set();
            });
            var makeTea = Task.Factory.StartNew(() =>
            {
                WriteLine("waiting for water ...");
                mre.Wait();
                WriteLine("Here is your tea");
            });
            makeTea.Wait();

            WriteLine("\n");

            Task.Factory.StartNew(() =>
            {
                WriteLine("going to a cafe");
                are.Set();
            });
            var makeCoffee = Task.Factory.StartNew(() =>
            {
                WriteLine("waiting in the cafe ...");
                are.WaitOne();
                WriteLine("Here is your coffee");
                var ok = are.WaitOne(1000);
                WriteLine(ok ? "Enjoy your coffee" : "Sorry. No Coffee for you!");
            });
            makeCoffee.Wait();
        }
    }
}
