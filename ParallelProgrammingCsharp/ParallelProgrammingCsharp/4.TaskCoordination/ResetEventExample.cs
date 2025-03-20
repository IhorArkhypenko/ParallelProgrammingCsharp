namespace ParallelProgrammingCsharp._4.TaskCoordination;

public class ResetEventExample : IExample
{
    public static void Run()
    {
        Console.WriteLine("ManualResetEventSlim example.");
        RunManualResetEventSlimExample();

        Console.WriteLine("\n\nAutoResetEvent example.");
        AutoResetEventExample();
    }

    private static void RunManualResetEventSlimExample()
    {
        var evt = new ManualResetEventSlim();

        Task.Factory.StartNew(() =>
        {
            Console.WriteLine("Boiling water.");
            evt.Set();
        });

        var makeTea = Task.Factory.StartNew(() =>
        {
            Console.WriteLine("Waiting for water.");
            evt.Wait(); // Change to true and never become false again. So even we will right more evt.Wait(); nothing will change.
            Console.WriteLine("Here is your tea.");
        });

        makeTea.Wait();
    }

    private static void AutoResetEventExample()
    {
        var evt = new AutoResetEvent(false);

        Task.Factory.StartNew(() =>
        {
            Console.WriteLine("Boiling water.");
            evt.Set(); // Change evt state to true.
        });

        var makeTea = Task.Factory.StartNew(() =>
        {
            Console.WriteLine("Waiting for water.");

            evt.WaitOne(); // Automatically change evt state back to false.

            var ok = evt.WaitOne(1000); // Will be false forever because no evt.Set() used to evt become true. Timeout solve this problem.
            Console.WriteLine(ok ? "Enjoy your tea." : "No tea for you.");
        });

        makeTea.Wait();
    }
}