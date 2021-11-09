using System;
using System.Threading;

namespace Mutex_Global_for_Application
{
    class Program
    {
        static void Main()
        {
            const string appName = "My application";

            Mutex mutex;

            try
            {
                mutex = Mutex.OpenExisting(appName);
                Console.WriteLine($"Sorry, {appName} is already running");
            }
            catch (WaitHandleCannotBeOpenedException)
            {
                Console.WriteLine("We can run the program just fine");
                mutex = new Mutex(false, appName);
            }

            Console.ReadKey();
            mutex.ReleaseMutex();
        }
    }
}
