using System.Collections.Concurrent;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using Perfolizer.Horology;

namespace ParallelProgrammingCsharp._5.ParallelLoops;

public class PartitioningExample : IExample
{
    public static void Run()
    {
        var config = ManualConfig
            .Create(DefaultConfig.Instance)
            .WithSummaryStyle(SummaryStyle.Default.WithTimeUnit(TimeUnit.Millisecond));

        var summary = BenchmarkRunner.Run<PartitioningExample>(config);
        Console.WriteLine(summary);
    }

    [Benchmark]
    public void SquareEachValue()
    {
        const int count = 100000;
        var values = Enumerable.Range(0, count);
        var results = new int[count];

        // We are making a lot of delegates here. Better avoid it.
        Parallel.ForEach(values, i => { results[i] = (int)Math.Pow(i, 2); });
    }

    [Benchmark]
    public void SquareEachValueChunked()
    {
        const int count = 100000;
        var values = Enumerable.Range(0, count);
        var results = new int[count];

        var part = Partitioner.Create(0, count, 10000);
        Parallel.ForEach(part, range =>
        {
            for (int i = range.Item1; i < range.Item2; i++)
            {
                results[i] = (int)Math.Pow(i, 2);
            }
        });
    }
}