using System.Collections.Concurrent;

namespace ParallelProgrammingCsharp.ConcurrentCollections;

public class ConcurrentStackExample
{
    public static void Run()
    {
        var stack = new ConcurrentStack<int>();
        stack.Push(1);
        stack.Push(2);
        stack.Push(3);
        stack.Push(4);

        int result;

        if (stack.TryPeek(out result))
        {
            Console.WriteLine($"{result} is on top.");
        }

        if (stack.TryPop(out result))
        {
            Console.WriteLine($"Popped the {result}");
        }

        var items = new int[5];
        if (stack.TryPopRange(items, 0, 5) > 0) // TryPopRange() returns number of popped elements
        {
            var text = string.Join(", ", items);
            Console.WriteLine($"Popped these elements: {text}"); // As we can see, last 2 els will be 0(default values)
        }
    }
}