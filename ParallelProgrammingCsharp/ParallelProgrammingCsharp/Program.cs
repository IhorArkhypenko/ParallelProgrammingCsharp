namespace ParallelProgrammingCsharp;

class Program
{
    static void Main(string[] args)
    {
        var cts = new CancellationTokenSource();
        var token = cts.Token;

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

        Console.ReadKey();
        cts.Cancel();

        Console.ReadKey();
    }
}