namespace ParallelProgrammingCsharp.TaskCoordination;

public class ChildTaskExample : IExample
{
    public static void Run()
    {
        var parent = new Task(() =>
        {
            // Detached.
            var child = new Task(() =>
                {
                    Console.WriteLine("Child task starting.");
                    Thread.Sleep(3000);
                    // Uncomment line below to test failHandler.
                    // throw new Exception();
                    Console.WriteLine("Child task finishing.");
                },
                TaskCreationOptions.AttachedToParent);

            var completionHandler = child.ContinueWith(t =>
                {
                    Console.WriteLine($"Task {t.Id}'s state is {t.Status}");
                },
                TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.OnlyOnRanToCompletion);

            var failHandler = child.ContinueWith(t =>
                {
                    Console.WriteLine($"Oops! Task {t.Id}'s state is {t.Status}");
                },
            TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.OnlyOnFaulted);


            child.Start();
        });

        parent.Start();

        try
        {
            parent.Wait();
        }
        catch (AggregateException ae)
        {
            ae.Handle(e => true);
        }
    }
}