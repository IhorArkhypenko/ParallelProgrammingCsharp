namespace ParallelProgrammingCsharp._1.TaskProgramming.ExceprionHandling;

public class TaskExceptionHandlingExample : IExample
{
    public static void Run()
    {
        var task1 = Task.Factory.StartNew(() => throw new InvalidOperationException("Can't do this!") { Source = "task1" });
        var task2 = Task.Factory.StartNew(() => throw new AccessViolationException() { Source = "task2" });

        try
        {
            Task.WaitAll(task1, task2);
        }
        catch (AggregateException ae)
        {
            foreach (var exception in ae.InnerExceptions)
            {
                Console.WriteLine($"Exception: {exception.GetType()} from {exception.Source}");
            }
        }
    }
}