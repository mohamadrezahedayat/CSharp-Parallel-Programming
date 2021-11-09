using System.Threading;
using System.Threading.Tasks;
using static System.Console;
using System.Collections.Generic;

namespace Interlocked_Operations
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
            Interlocked.Add(ref _balance, amount);
        }
        public void Withdraw(int amount)
        {
            Interlocked.Add(ref _balance, -amount);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var tasks = new List<Task>();
            var ba = new BankAccount();
            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        ba.Deposit(100);
                    }
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        ba.Withdraw(100);
                    }
                }));
                Task.WaitAll(tasks.ToArray());
            }
            WriteLine($"Final Balance is : {ba.Balance}");
            ReadKey();
        }
    }
}
