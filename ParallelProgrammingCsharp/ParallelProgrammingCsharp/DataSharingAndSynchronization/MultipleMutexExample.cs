namespace ParallelProgrammingCsharp.DataSharingAndSynchronization;

public class MultipleMutexExample
{
    private class BankAccount
    {
        public int Balance { get; private set; }

        public void Deposit(int amount)
        {
            Balance += amount;
        }

        public void Withdraw(int amount)
        {
            Balance -= amount;
        }

        public void Transfer(BankAccount targetBankAccount, int amount)
        {
            Balance -= amount;
            targetBankAccount.Balance += amount;
        }
    }

    public static void Run()
    {
        var bankAccount = new BankAccount();
        var bankAccount2 = new BankAccount();
        var taskList = new List<Task>();

        var mutex = new Mutex();
        var mutex2 = new Mutex();

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 1000; j++)
            {
                taskList.Add(Task.Factory.StartNew(() =>
                {
                    bool haveLock = mutex.WaitOne();

                    try
                    {
                        bankAccount.Deposit(100);
                    }
                    finally
                    {
                        if (haveLock)
                        {
                            mutex.ReleaseMutex();
                        }
                    }
                }));
            }

            for (int j = 0; j < 1000; j++)
            {
                taskList.Add(Task.Factory.StartNew(() =>
                {
                    bool haveLock = mutex2.WaitOne();

                    try
                    {
                        bankAccount2.Deposit(100);
                    }
                    finally
                    {
                        if (haveLock)
                        {
                            mutex2.ReleaseMutex();
                        }
                    }
                }));
            }

            for (int j = 0; j < 1000; j++)
            {
                taskList.Add(Task.Factory.StartNew(() =>
                {
                    bool haveLock = WaitHandle.WaitAll(new WaitHandle[] { mutex, mutex2 });

                    try
                    {
                        bankAccount.Transfer(bankAccount2, 100);
                    }
                    finally
                    {
                        if (haveLock)
                        {
                            mutex.ReleaseMutex();
                            mutex2.ReleaseMutex();
                        }
                    }
                }));
            }
        }

        Task.WaitAll(taskList.ToArray());

        Console.WriteLine($"Balance 1: {bankAccount.Balance}");
        Console.WriteLine($"Balance 2: {bankAccount2.Balance}");
    }
}