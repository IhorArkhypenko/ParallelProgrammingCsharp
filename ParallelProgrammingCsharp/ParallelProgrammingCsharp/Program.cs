using ParallelProgrammingCsharp.DataSharingAndSynchronization;
using ParallelProgrammingCsharp.TaskProgramming;

namespace ParallelProgrammingCsharp;

class Program
{
    static void Main(string[] args)
    {
        // Task programming.
        CreateAndRunTask.Run();
        
        // Data sharing and synchronization.
        CriticalSection.Run();
    }
}