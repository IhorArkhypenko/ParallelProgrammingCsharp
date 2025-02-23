namespace ParallelProgrammingCsharp;

class Program
{
    static int TextLengh(object? o)
    {
        Console.WriteLine($"Task {Task.CurrentId} processing object {o}...");
        return o.ToString().Length;
    }
    
    static void Main(string[] args)
    {
        string text1 = "test";
        string text2 = "testing";
        
        var task1 = Task.Factory.StartNew(TextLengh, text1);
        var task2 = new Task<int>(TextLengh, text2);
        task2.Start();

        Console.WriteLine($"Length of {text1} is {task1.Result}");
        Console.WriteLine($"Length of {text2} is {task2.Result}");
    }
}