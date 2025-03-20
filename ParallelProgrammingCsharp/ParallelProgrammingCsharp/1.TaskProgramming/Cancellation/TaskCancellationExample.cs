namespace ParallelProgrammingCsharp._1.TaskProgramming.Cancellation;

public class TaskCancellationExample : IExample
{
    public static void Run()
    {
        var cts = new CancellationTokenSource();
        var token = cts.Token;

        // First way to understand that cancellation requested.
        token.Register(() => Console.WriteLine("Cancellation requested"));

        var task = new Task(() =>
        {
            var i = 0;
            while (true)
            {
                // The same thing as if statement below, but shorter.
                //cts.Token.ThrowIfCancellationRequested();

                if(cts.IsCancellationRequested)
                {
                    throw new TaskCanceledException();
                }
                else
                {
                    Console.WriteLine($"{i++} \t");
                }
            }
        }, token);
        
        task.Start();

        // Second way to understand that task was cancelled(more complicated).
        Task.Factory.StartNew(() =>
        {
            token.WaitHandle.WaitOne();
            Console.WriteLine("Wait handle released, cancellation was requested");
        });

        Console.ReadKey();
        cts.Cancel();
        
        Console.ReadKey();
    }
}