namespace ParallelProgrammingCsharp.DataSharingAndSynchronization;

public class InterlockedExample
{
    private class BankAccount
    {
        private int _balance;

        public int Balance
        {
            get => _balance;
            private set => _balance = value;
        }

        public void Deposit(int amount)
        {
            // 'ref' argument must be an assignable variable, field, or an array element. It is why we need a backing field.
            Interlocked.Add(ref _balance, amount);
        }

        public void Withdraw(int amount)
        {
            Interlocked.Add(ref _balance, -amount);
        }
    }

    public static void Run()
    {
        var bankAccount = new BankAccount();
        var taskList = new List<Task>();

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 1000; j++)
            {
                var task = Task.Factory.StartNew(() => bankAccount.Deposit(100));
                taskList.Add(task);
            }

            for (int j = 0; j < 1000; j++)
            {
                var task = Task.Factory.StartNew(() => bankAccount.Withdraw(100));
                taskList.Add(task);
            }
        }

        Task.WaitAll(taskList.ToArray());

        Console.WriteLine($"Balance: {bankAccount.Balance}");
    }
}