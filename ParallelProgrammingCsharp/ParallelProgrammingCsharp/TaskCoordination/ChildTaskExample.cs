namespace ParallelProgrammingCsharp.TaskCoordination;

public class ChildTaskExample : IExample
{
    public static void Run()
    {
        var parent = new Task(() =>
        {
            // detached
            var child = new Task(() =>
            {
                Console.WriteLine("Child task starting.");
                Thread.Sleep(3000);
                Console.WriteLine("Child task finishing.");
            });
            child.Start();
        });

        parent.Start();
    }
}