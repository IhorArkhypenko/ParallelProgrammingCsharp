namespace ParallelProgrammingCsharp;

class Program
{
    static void Write(char c)
    {
        int i = 1000;
        while (i-- > 0)
        {
            Console.Write(c);
        }
    }

    static void Write(object obj)
    {
        int i = 1000;
        while (i-- > 0)
        {
            Console.Write(obj);
        }
    }
    
    static void Main(string[] args)
    {
        Task.Factory.StartNew(Write, "hello");

        var task = new Task(Write, 123);
        task.Start();

        Console.WriteLine("Main program done.");
        Console.ReadKey();
    }
}