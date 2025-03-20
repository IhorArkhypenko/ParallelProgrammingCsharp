namespace ParallelProgrammingCsharp._4.TaskCoordination;

public class BarrierExample : IExample
{
    private static readonly Barrier _barrier = new Barrier(2, b =>
    {
        Console.WriteLine($"Phase {b.CurrentPhaseNumber} is finished");
    });

    private static void Cup()
    {
        // 0 phase
        Console.WriteLine("Finding the nicest cup of tea (fast)");
        _barrier.SignalAndWait();
        // 1 phase
        Console.WriteLine("Adding tea");
        _barrier.SignalAndWait();
        // 2 phase
        Console.WriteLine("Adding sugar");
    }

    private static void Water()
    {
        // 0 phase
        Console.WriteLine("Putting the kettle on (takes a bit longer)");
        Thread.Sleep(2000);
        _barrier.SignalAndWait();
        // 1 phase
        Console.WriteLine("Pouring water into cup");
        _barrier.SignalAndWait();
        // 2 phase
        Console.WriteLine("Putting the kettle away");
    }

    public static void Run()
    {
        var water = Task.Factory.StartNew(Water);
        var cup = Task.Factory.StartNew(Cup);

        var tea = Task.Factory.ContinueWhenAll(new[] { water, cup }, tasks =>
        {
            Console.WriteLine("Enjoy your cup of tea.");
        });

        tea.Wait();
        // There no message "Phase 2 is finished because we have no SignalAndWait() after last actions."
    }
}