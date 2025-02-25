namespace ParallelProgrammingCsharp;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            Test();
        }
        catch (AggregateException ae)
        {
            // Handling unhandled AccessViolationException.
            foreach (var exception in ae.InnerExceptions)
            {
                Console.WriteLine($"Handled elsewhere: {exception.GetType()}");
            }
        }

        Console.WriteLine("Main program finished.");
        Console.ReadKey();
    }

    private static void Test()
    {
        var task1 = Task.Factory.StartNew(() => throw new InvalidOperationException("Can't do this!") { Source = "task1" });
        var task2 = Task.Factory.StartNew(() => throw new AccessViolationException() { Source = "task2" });

        try
        {
            Task.WaitAll(task1, task2);
        }
        catch (AggregateException ae)
        {
            // Way to handle only part of exceptions.
            ae.Handle(e =>
            {
                if (e is InvalidOperationException)
                {
                    Console.WriteLine("InvalidOperationException!");
                    return true;
                }
                else
                {
                    return false;
                }
            });
        }
    }
}