namespace ParallelProgrammingCsharp._6.ParallelLinq;

public class AsParallelAndParallelQueryExample : IExample
{
    public static void Run()
    {
        // AsParallel example.
        const int count = 50;
        var items = Enumerable.Range(0, count).ToArray();
        var results = new int[count];

        items.AsParallel().ForAll(i =>
        {
            results[i] = (int)Math.Pow(i, 3);
            // Results will be displayed in random order because of concurrency.
            Console.WriteLine($"Result: {results[i]}, Task: {Task.CurrentId}");
        });

        Console.WriteLine();

        foreach (var item in results)
        {
            // Results will be displayed in correct order because we wrote it to corresponding cells.
            Console.WriteLine(item);
        }

        // AsOrdered() example.
        var cubes = items
            .AsParallel()
            .AsOrdered()
            .Select(x => (int)Math.Pow(x, 3));

        foreach (var item in cubes)
        {
            Console.Write($"{item}\t");
        }
    }
}