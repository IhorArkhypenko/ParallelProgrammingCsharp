namespace ParallelProgrammingCsharp.DataSharingAndSynchronization;

public class SingleMutexExample : IExample
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
    }

    public static void Run()
    {
        var bankAccount = new BankAccount();
        var taskList = new List<Task>();

        var mutex = new Mutex();

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 1000; j++)
            {
                taskList.Add(Task.Factory.StartNew(() =>
                {
                    bool haveLock = mutex.WaitOne();

                    try
                    {
                        bankAccount.Withdraw(100);
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
        }

        Task.WaitAll(taskList.ToArray());

        Console.WriteLine($"Balance: {bankAccount.Balance}");
    }
}