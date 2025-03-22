namespace ParallelProgrammingCsharp._6.ParallelLinq;

public class MergeOptionsExample : IExample
{
    public static void Run()
    {
        var numbers = Enumerable.Range(1, 20).ToArray();

        // Merge options let you control how quickly you gets your results once they are calculated.
        var results = numbers
            .AsParallel()
            .WithMergeOptions(ParallelMergeOptions.FullyBuffered)
            .Select(x =>
            {
                var result = Math.Log10(x);
                Console.WriteLine($"Produced {result}");
                return result;
            });

        foreach (var item in results)
        {
            Console.WriteLine($"Consumed {item}");
        }
    }
}