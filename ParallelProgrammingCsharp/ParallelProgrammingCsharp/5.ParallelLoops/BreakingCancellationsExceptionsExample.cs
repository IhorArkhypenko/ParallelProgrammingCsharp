namespace ParallelProgrammingCsharp._5.ParallelLoops;

public class BreakingCancellationsExceptionsExample : IExample
{
    public static void Run()
    {
        try
        {
            Demo();
        }
        catch (AggregateException ae)
        {
            ae.Handle(e =>
            {
                Console.WriteLine(e.Message);
                return true;
            });
        }
    }

    private static void Demo()
    {
        var cts = new CancellationTokenSource();

        var po = new ParallelOptions { CancellationToken = cts.Token };

        var result = Parallel.For(0, 20, po, (x, state) =>
        {
            Console.WriteLine($"{x}[{Task.CurrentId}]");
            if (x == 10)
            {
                // throw new Exception();
                // state.Stop(); // Aggressive stopping of the loop. Stops execution of loop as soon as possible.
                state.Break(); // Soft breaking of the loop. It can guarantee that started iterations will be finished and new will not be started.
                // cts.Cancel(); // If you want to cancel task this way, OperationCanceledException will be thrown and will not be converted to AggregateException.
            }
        });

        Console.WriteLine($"\nWas Loop completed? {result.IsCompleted}");
        if (result.LowestBreakIteration.HasValue)
        {
            Console.WriteLine($"Lowest Break Iteration: {result.LowestBreakIteration}");
        }
    }
}