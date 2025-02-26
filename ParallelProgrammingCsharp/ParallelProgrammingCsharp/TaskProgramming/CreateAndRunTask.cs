namespace ParallelProgrammingCsharp.TaskProgramming;

public class CreateAndRunTask
{
    static int TextLengh(object? o)
    {
        Console.WriteLine($"Task {Task.CurrentId} processing object {o}...");
        return o.ToString().Length;
    }
    
    public static void Run()
    {
        string text1 = "test";
        string text2 = "testing";

        // First way (starts immediately)
        var task1 = Task.Factory.StartNew(TextLengh, text1);
        // Second way (need to start manually using task.Start();) 
        var task2 = new Task<int>(TextLengh, text2); 
        task2.Start();

        Console.WriteLine($"Length of {text1} is {task1.Result}");
        Console.WriteLine($"Length of {text2} is {task2.Result}");
    }
}