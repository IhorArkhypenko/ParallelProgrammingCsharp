namespace ParallelProgrammingCsharp._2.DataSharingAndSynchronization;

public class ReaderWriterLockExample : IExample
{
    private static readonly ReaderWriterLockSlim _padLock = new();
    private static readonly Random _random = new();

    public static void Run()
    {
        int x = 0;

        var tasks = new List<Task>();

        for (int i = 0; i < 10; i++)
        {
            tasks.Add(Task.Factory.StartNew(() =>
            {
                _padLock.EnterReadLock();

                Console.WriteLine($"Entered read lock, x = {x}");
                Thread.Sleep(5000);

                _padLock.ExitReadLock();
                Console.WriteLine($"Exited read lock, x = {x}");
            }));
        }

        try
        {
            Task.WaitAll(tasks.ToArray());
        }
        catch (AggregateException ae)
        {
            ae.Handle(e =>
            {
                Console.WriteLine();
                return true;
            });
        }

        while (true)
        {
            Console.ReadKey();
            _padLock.EnterWriteLock();
            Console.Write("Write lock acquired");
            int newValue = _random.Next(10);
            x = newValue;
            Console.WriteLine($"Set x = {x}");
            _padLock.ExitWriteLock();
            Console.WriteLine("Write lock released");
        }
    }
}