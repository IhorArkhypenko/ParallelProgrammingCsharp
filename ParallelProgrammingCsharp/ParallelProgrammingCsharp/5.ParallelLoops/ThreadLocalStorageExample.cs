namespace ParallelProgrammingCsharp._5.ParallelLoops;

public class ThreadLocalStorageExample : IExample
{
    public static void Run()
    {
        // Way to reduce number of calls to the final variable. (sum in this example)
        var sum = 0;

        Parallel.For(1, 1001,
            () => 0,
            (x, state, tls) =>
            {
                tls += x;
                Console.WriteLine($"Task {Task.CurrentId} has sum {tls}");
                return tls;
            },
            partialSum =>
            {
                Console.WriteLine($"Partial value of task {Task.CurrentId} is {partialSum}");
                Interlocked.Add(ref sum, partialSum);
            });

        Console.WriteLine($"Sum of 1...1000 = {sum}");
    }
}