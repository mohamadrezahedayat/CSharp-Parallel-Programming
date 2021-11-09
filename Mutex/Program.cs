﻿using static System.Console;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Mutex_project
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

        public void Transfer(BankAccount where, int amount)
        {
            Balance -= amount;
            where.Balance += amount;
        }
    }
    class Program
    {
        static void Main()
        {
            var tasks = new List<Task>();
            var ba = new BankAccount();
            var ba2 = new BankAccount();

            Mutex mutex = new Mutex();
            Mutex mutex2 = new Mutex();

            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        bool haveLock = mutex.WaitOne();
                        try
                        {
                            ba.Deposit(1);
                        }
                        finally
                        {
                            if(haveLock) mutex.ReleaseMutex();
                        }
                    }
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        bool haveLock = mutex2.WaitOne();
                        try
                        {
                            ba2.Deposit(1);

                        }
                        finally
                        {
                            if (haveLock) mutex2.ReleaseMutex();
                        }
                    }
                }));
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        bool haveLock = WaitHandle.WaitAll(new[]{mutex,mutex2});
                        try
                        {
                            ba.Transfer(ba2,1);

                        }
                        finally
                        {
                            if (haveLock)
                            {
                                mutex.ReleaseMutex();
                                mutex2.ReleaseMutex();
                            }
                        }
                    }
                }));
                Task.WaitAll(tasks.ToArray());
            }
            WriteLine($"Final bank1 Balance is : {ba.Balance}");
            WriteLine($"Final bank2 Balance is : {ba2.Balance}");
            ReadKey();
        }
    }
}
