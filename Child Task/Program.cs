using System;
using System.Threading;
using System.Threading.Tasks;

namespace Child_Task
{
    class Program
    {
        static void Main()
        {
            var parent = new Task(() =>
            {
                // detached
                var child = new Task(() =>
                {
                    Console.WriteLine("Child task starting.");
                    Thread.Sleep(3000);
                    Console.WriteLine("Child task finishing.");
                    //throw new Exception();
                },TaskCreationOptions.AttachedToParent);

                var completionHandler = child.ContinueWith(t =>
                    {
                        Console.WriteLine($"Hooray, task {t.Id}'s state is {t.Status}");
                    },TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.OnlyOnRanToCompletion
                );

                var failHandler = child.ContinueWith(t =>
                    {
                        Console.WriteLine($"Oops, task {t.Id}'s state is {t.Status}");
                    }, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.OnlyOnFaulted
                );
                child.Start();
            });
            parent.Start();
            try
            {
                parent.Wait();
            }
            catch (AggregateException ae)
            {
               ae.Handle((e)=>true);
            }
        }
    }
}
