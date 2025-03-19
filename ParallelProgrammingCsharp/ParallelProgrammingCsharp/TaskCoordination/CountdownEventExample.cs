namespace ParallelProgrammingCsharp.TaskCoordination;

public class CountdownEventExample : IExample
{
    private const int _taskCount = 5;
    private static readonly CountdownEvent _cte = new CountdownEvent(_taskCount);
    private static readonly Random _random = new();

    public static void Run()
    {
        for (int i = 0; i < _taskCount; i++)
        {
            Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"Entering task {Task.CurrentId}");
                Thread.Sleep(_random.Next(3000));
                _cte.Signal();
                Console.WriteLine($"Exiting task {Task.CurrentId}");
            });
        }

        var finalTask = Task.Factory.StartNew(() =>
        {
            Console.WriteLine($"Waiting for other tasks to complete in {Task.CurrentId}");
            _cte.Wait();
            Console.WriteLine("All tasks completed");
        });

        finalTask.Wait();
    }
}