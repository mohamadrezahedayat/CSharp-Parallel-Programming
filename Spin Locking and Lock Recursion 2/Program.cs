using System;
using System.Threading;

namespace Spin_Locking_and_Lock_Recursion_2
{
    class Program
    {
        static SpinLock _sl = new SpinLock(true);
        public static void LockRecursion(int x)
        { 
            bool lockTaken = false;
            try
            {
                _sl.Enter(ref lockTaken);
            }
            catch (LockRecursionException e)
            {
                Console.WriteLine("Exception: "+ e);
            }
            finally
            {
                if (lockTaken)
                {
                    Console.WriteLine($"Took a lock, c = {x}");
                    LockRecursion(x-1);
                    _sl.Exit();
                }
                else
                {
                    Console.WriteLine($"Failed to take a lock, x = {x}");
                }
            }
        }

        private static void Main()
        {
           LockRecursion(5);
        }

     
    }
}
