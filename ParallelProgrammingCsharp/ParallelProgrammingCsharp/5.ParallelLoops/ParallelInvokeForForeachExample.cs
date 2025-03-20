namespace ParallelProgrammingCsharp._5.ParallelLoops;

public class ParallelInvokeForForeachExample : IExample
{
    public static void Run()
    {
        Console.WriteLine("Parallel.Invoke() example.");
        RunParallelInvokeExample();

        Console.WriteLine("\nParallel.For() example.");
        RunParallelForExample();

        Console.WriteLine("\nParallel.ForEach() example.");
        RunParallelForeachExample();

        Console.WriteLine("\nParallel.For() with custom step example.");
        RunParallelForWithCustomStepExample();
    }

    private static void RunParallelInvokeExample()
    {
        var a = new Action(() => Console.WriteLine($"First (Task id: {Task.CurrentId})"));
        var b = new Action(() => Console.WriteLine($"Second (Task id: {Task.CurrentId})"));
        var c = new Action(() => Console.WriteLine($"Third (Task id: {Task.CurrentId})"));

        Parallel.Invoke(a, b, c);
    }

    private static void RunParallelForExample()
    {
        Parallel.For(1, 20, i =>
        {
            Console.WriteLine($"{i * i}\t");
        });
    }

    private static void RunParallelForeachExample()
    {
        var words = new string[] { "oh", "ah", "eh", "uh" };

        Parallel.ForEach(words, word =>
        {
            Console.WriteLine($"{word} length is {word.Length}. (task {Task.CurrentId})");
        });
    }

    private static void RunParallelForWithCustomStepExample()
    {
        Parallel.ForEach(Range(1, 10, 2), i =>
        {
            Console.WriteLine($"{i}\t");
        });
    }

    private static IEnumerable<int> Range(int start, int end, int step)
    {
        for (int i = start; i < end; i += step)
        {
            yield return i;
        }
    }
}