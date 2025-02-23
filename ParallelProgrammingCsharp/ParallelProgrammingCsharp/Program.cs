namespace ParallelProgrammingCsharp;

class Program
{
    static void Main(string[] args)
    {
        var cts = new CancellationTokenSource();
        var token = cts.Token;
        
        token.Register(() => Console.WriteLine("Cancellation requested"));

        var task = new Task(() =>
        {
            var i = 0;
            while (true)
            {
                cts.Token.ThrowIfCancellationRequested();
                Console.WriteLine($"{i++} \t");
            }
        }, token);

        task.Start();

        // second way to understand that task was cancelled(more complicated)
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