using System.Threading;
using System.Threading.Tasks;
using static System.Console;
using System.Collections.Generic;

namespace Spin_Locking_and_Lock_Recursion
{
    public class BankAccount
    {
        private int _balance;

        public int Balance
        {
            get => _balance;
            private set => _balance = value;
        }
        public void Deposit(int amount)
        {
            _balance += amount;
        }
        public void Withdraw(int amount)
        {
            _balance -= amount;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var tasks = new List<Task>();
            var ba = new BankAccount();

            SpinLock sl = new SpinLock();

            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        var lockTaken = false;
                        try
                        {
                            sl.Enter(ref lockTaken);
                            ba.Deposit(100);
                        }
                        finally
                        {
                            if (lockTaken) sl.Exit();
                        }
                      
                    }
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        var lockTaken = false;
                        try
                        {
                            sl.Enter(ref lockTaken);
                            ba.Withdraw(100);
                        }
                        finally
                        {
                            if (lockTaken) sl.Exit();
                        }
                       
                    }
                }));
                Task.WaitAll(tasks.ToArray());
            }
            WriteLine($"Final Balance is : {ba.Balance}");
            ReadKey();
        }
    }
}
