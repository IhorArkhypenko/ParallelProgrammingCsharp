using System.Collections.Concurrent;

namespace ParallelProgrammingCsharp.ConcurrentCollections;

public class ConcurrentDictionaryExample : IExample
{
    private static ConcurrentDictionary<string, string> _capitals = new();

    private static void AddParis()
    {
        bool isSuccess = _capitals.TryAdd("France", "Paris");
        string caller = Task.CurrentId.HasValue ? ("Task" + Task.CurrentId) : "Main thread";

        Console.WriteLine($"{caller} {(isSuccess ? "added" : "did not add")} the element.");
    }

    public static void Run()
    {
        // TryAdd()
        Task.Factory.StartNew(AddParis).Wait();
        AddParis();

        // AddOrUpdate()
        _capitals["Ukraine"] = "Kharkiv";

        // delegate(last parameter) for updating case. To try - comment previous line.
        _capitals.AddOrUpdate("Ukraine", "Kyiv", (key, old) => old + " ==> Kyiv");

        Console.WriteLine($"The capital of Ukraine is {_capitals["Ukraine"]}");

        // GetOrAdd()
        _capitals["Germany"] = "Berlin";
        var capOfGermany = _capitals.GetOrAdd("Germany", "OtherValue");

        Console.WriteLine($"The capital of Germany is {_capitals["Germany"]}");

        // TryRemove()
        const string capitalToRemove = "Germany";

        var isRemoved = _capitals.TryRemove(capitalToRemove, out var removedCapital);

        if (isRemoved)
        {
            Console.WriteLine($"The capital of {capitalToRemove}({removedCapital}) was removed.");
        }
        else
        {
            Console.WriteLine($"Failed to remove the capital of {capitalToRemove}");
        }
    }
}