namespace ParallelProgrammingCsharp;

class Program
{
    public static void Write(char c)
    {
        int i = 1000;
        while (i-- > 0)
        {
            Console.Write(c);
        }
    }
    
    static void Main(string[] args)
    {
        // First way of creating and starting Task at the same time.
        Task.Factory.StartNew(() => Write('1'));
        
        // Second way of creating Task without starting it.
        var task = new Task(() => Write('2'));
        task.Start(); // Starting of the second Task in other thread.
        
        Write('3'); // Operation in the main thread.
        
        Console.WriteLine("Main program done.");
        Console.ReadKey();
    }
}