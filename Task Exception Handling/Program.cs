using System;
using System.Threading.Tasks;
using static System.Console;

namespace Task_Exception_Handling
{
    class Program
    {
        static void Main()
        {
            try
            {
                Demo();
            }
            catch (AggregateException ae)
            {
                foreach (var e in ae.InnerExceptions)
                {
                    WriteLine($"Handles elsewhere: {e.GetType()}");
                }
            }

            WriteLine("Main program done.");
            ReadKey();
        }

        private static void Demo()
        {
            var t1 = Task.Factory.StartNew(() =>
                throw new InvalidOperationException("Can't Do this!") {Source = "t1"});

            var t2 = Task.Factory.StartNew(() =>
                throw new AccessViolationException("Can't Access this!") {Source = "t2"});
            try
            {
                Task.WaitAll(t1, t2);
            }
            catch (AggregateException ae)
            {
                ae.Handle(e =>
                {
                    if (e is InvalidOperationException)
                    {
                        WriteLine("Invalid op!");
                        return true;
                    }

                    return false;
                });
            }
        }
    }
}
