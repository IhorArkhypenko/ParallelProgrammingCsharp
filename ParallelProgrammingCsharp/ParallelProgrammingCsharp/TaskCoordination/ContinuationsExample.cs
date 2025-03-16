namespace ParallelProgrammingCsharp.TaskCoordination;

public class ContinuationsExample
{
    public static void Run()
    {
        // Way to start one task after another.
        var task = Task.Factory.StartNew(() =>
        {
            Console.WriteLine("Boiling water!");
        });

        var task2 = task.ContinueWith(t =>
        {
            Console.WriteLine($"Water boiled by task {t.Id}. Ready to serve in cups!");
        });

        task2.Wait();

        // Way to continue after more than one task will be completed. (Also method ContinueWithAny() exists.)
        var t = Task.Factory.StartNew(() => "Task 1");
        var t2 = Task.Factory.StartNew(() => "Task 2");

        var t3 = Task.Factory.ContinueWhenAll(
            new[] { t, t2 },
            tasks =>
        {
            Console.WriteLine("Tasks completed:");
            foreach (var item in tasks)
            {
                Console.WriteLine("- " + item.Result);
            }
            Console.WriteLine("All tasks done.");
        });

        t3.Wait();
    }
}