using System.Collections.Concurrent;

namespace ParallelProgrammingCsharp._3.ConcurrentCollections;

// Just a wrapper for other collections which implement IProducerConsumer to set fixed maxCountValue.
public class BlockingCollectionExample : IExample
{
    private static BlockingCollection<int> _messages = new(new ConcurrentBag<int>(), 10);
    private static readonly CancellationTokenSource _cts = new();
    private static Random _random = new();

    public static void Run()
    {
        Task.Factory.StartNew(ProduceAndConsume, _cts.Token);

        Console.ReadKey();
        _cts.Cancel();
    }

    private static void ProduceAndConsume()
    {
        var producer = Task.Factory.StartNew(RunProducer);
        var consumer = Task.Factory.StartNew(RunConsumer);

        try
        {
            Task.WaitAll(new[] { producer, consumer }, _cts.Token);
        }
        catch (AggregateException ae)
        {
            ae.Handle(e => true);
        }
    }

    private static void RunConsumer()
    {
        foreach (var item in _messages.GetConsumingEnumerable())
        {
            _cts.Token.ThrowIfCancellationRequested();
            Console.WriteLine($"-{item}\t");
            Thread.Sleep(_random.Next(1000));
        }
    }

    private static void RunProducer()
    {
        while (true)
        {
            _cts.Token.ThrowIfCancellationRequested();

            var randomValue = _random.Next(100);
            _messages.Add(randomValue);
            Console.WriteLine($"+{randomValue}\t");

            Thread.Sleep(_random.Next(1000)); // We can reduce parameter to 100 to test 10 max capacity.
        }
    }
}