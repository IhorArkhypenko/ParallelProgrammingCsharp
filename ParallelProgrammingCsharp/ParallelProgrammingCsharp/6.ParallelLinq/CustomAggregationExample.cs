namespace ParallelProgrammingCsharp._6.ParallelLinq;

public class CustomAggregationExample : IExample
{
    public static void Run()
    {
        // var sum = Enumerable.Range(1, 1000).Sum();

        // Sequential way.
        // var sum = Enumerable.Range(1, 1000)
        //     .Aggregate(0, (i, acc) => i + acc);

        // Parallel way.
        var sum = ParallelEnumerable.Range(1, 1000)
            .Aggregate(
                0,
                (partialSum, i) => partialSum += i,
                (total, subtotal) => total += subtotal,
                i => i);

        Console.WriteLine($"Sum = {sum}");
    }
}