namespace ParallelProgrammingCsharp.TaskProgramming;

public class WaitForATaskCompletionExample
{
    public static void Run()
    {
        var cts = new CancellationTokenSource();
        var token = cts.Token;

        var task = new Task(() =>
        {
            Console.WriteLine("I take 5 seconds.");

            for (int i = 0; i < 5; i++)
            {
                token.ThrowIfCancellationRequested();
                Thread.Sleep(1000);
            }

            Console.WriteLine("I am done!");
        }, token);
        task.Start();

        var task2 = Task.Factory.StartNew(() => Thread.Sleep(3000), token);

        // Will wait for all pending tasks.
        // Task.WaitAll(task, task2); 

        // Wait for particular task.
        // task.Wait(token);

        // Will wait for first completed task. 4000 is ms timeout(same available in WaitAll).
        // Task.WaitAny(new[] {task, task2}, 4000, token); 

        Console.WriteLine($"task status is {task.Status}");
        Console.WriteLine($"task2 status is {task2.Status}");

        Console.WriteLine("Main program done!");
        Console.ReadKey();
    }
}