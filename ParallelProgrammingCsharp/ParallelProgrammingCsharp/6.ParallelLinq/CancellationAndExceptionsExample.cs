namespace ParallelProgrammingCsharp._6.ParallelLinq;

public class CancellationAndExceptionsExample : IExample
{
    // Exceptions and cancellations need to be handled like everywhere in TPL.
    // Exceptions will not be combined in AggregateException.
    public static void Run()
    {
        var cts = new CancellationTokenSource();
        var items = ParallelEnumerable.Range(1, 20);

        var results = items
            .WithCancellation(cts.Token)
            .Select(x =>
            {
                var result = Math.Log10(x);

                Console.WriteLine($"x = {x}, Task id = {Task.CurrentId}");
                return result;
            });

        try
        {
            foreach (var item in results)
            {
                // InvalidOperationException need to be caught additionally.
                // if (item < 1)
                //     throw new InvalidOperationException();

                // OperationCanceledException need to be caught additionally.
                if (item > 1)
                    cts.Cancel();

                Console.WriteLine($"Result = {item}");
            }
        }
        catch (AggregateException ae)
        {
            ae.Handle(e =>
            {
                Console.WriteLine($"{e.GetType().Name}: {e.Message}");
                return true;
            });
        }
        catch(InvalidOperationException)
        {
            Console.WriteLine("IOE handled.");
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Canceled");
        }
    }
}