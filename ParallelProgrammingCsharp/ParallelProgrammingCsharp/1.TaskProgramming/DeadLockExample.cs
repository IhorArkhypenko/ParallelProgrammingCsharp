namespace ParallelProgrammingCsharp._1.TaskProgramming;

public class DeadLockExample : IExample
{
    // 2 objects for locking.
    private static readonly object resourceA = new();
    private static readonly object resourceB = new();

    public static void Run()
    {
        var task1 = Task.Run(Thread1Method);
        var task2 = Task.Run(Thread2Method);

        // Waiting for ending (never ends because of deadlock).
        Task.WaitAll(task1, task2);

        Console.WriteLine("Program finished."); // This line will never be reached.
    }

    private static void Thread1Method()
    {
        Console.WriteLine("Thread 1: Trying to grab resource A");
        lock (resourceA)
        {
            Console.WriteLine("Thread 1: Grabbed resource A");

            // Giving time for 1st thread to grab resource A.
            Thread.Sleep(100);

            Console.WriteLine("Thread 1: Trying to grab resource B");
            lock (resourceB)
            {
                Console.WriteLine("Thread 1: Grabbed resource B");
            }
        }
    }

    private static void Thread2Method()
    {
        Console.WriteLine("Thread 2: Trying to grab resource B");
        lock (resourceB)
        {
            Console.WriteLine("Thread 2: Grabbed resource B");

            // Giving time for 1st thread to grab resource A.
            Thread.Sleep(100);

            Console.WriteLine("Thread 2: Trying to grab resource A");
            lock (resourceA)
            {
                Console.WriteLine("Thread 2: Grabbed resource A");
            }
        }
    }
}