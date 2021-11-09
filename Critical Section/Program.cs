using static System.Console;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Critical_Section
{
    class Program
    {
        public class BankAccount
        {
            public object Padlock = new object();
            public int Balance;
            public void Deposit(int amount)
            {
                // +=
                // op1: temp <- get_Balance + amount
                // op2: set_Balance(temp)
                lock (Padlock)
                {
                    Balance += amount;
                }
            }
            public void Withdraw(int amount)
            {
                lock (Padlock)
                {
                    Balance -= amount;
                }
            }
        }
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
