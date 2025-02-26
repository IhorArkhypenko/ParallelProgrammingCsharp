namespace ParallelProgrammingCsharp.TaskProgramming.Cancellation;

public class LinkedTokenSource
{
    public static void Run()
    {
        var planned = new CancellationTokenSource();
        var preventative = new CancellationTokenSource();
        var emergency = new CancellationTokenSource();

        var paranoid = CancellationTokenSource.CreateLinkedTokenSource(planned.Token, preventative.Token, emergency.Token);

        Task.Factory.StartNew(() =>
        {
            int i = 0;
            while (true)
            {
                // Will be cancelled no matter which CancellationTokenSource will be cancelled because they all were linked to paranoid cts.
                paranoid.Token.ThrowIfCancellationRequested();
                Console.WriteLine($"{i++}\t");
                Thread.Sleep(1000);
            }
        });

        Console.ReadKey();
        emergency.Cancel();

        Console.ReadKey();
    }
}