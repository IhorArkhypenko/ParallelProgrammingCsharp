using System.Collections.Concurrent;

namespace ParallelProgrammingCsharp._3.ConcurrentCollections;

public class ConcurrentBagExample : IExample
{
    public static void Run()
    {
        var bag = new ConcurrentBag<int>();
        var tasks = new List<Task>();

        for (int i = 0; i < 10; i++)
        {
            var i1 = i;
            tasks.Add(Task.Factory.StartNew(() =>
            {
                bag.Add(i1);
                Console.WriteLine($"Task {Task.CurrentId} has added {i1}");

                int result;
                if (bag.TryPeek(out result))
                {
                    Console.WriteLine($"Task {Task.CurrentId} has picked {result}");
                }
            }));
        }

        Task.WaitAll(tasks.ToArray());
        // As we can see, each thread peeks the value which it previously added.

        int last;
        if (bag.TryTake(out last))
        {
            Console.WriteLine($"Task {Task.CurrentId} has removed {last}");
        }
    }
}