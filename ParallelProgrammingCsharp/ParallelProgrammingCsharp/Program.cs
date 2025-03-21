using ParallelProgrammingCsharp._5.ParallelLoops;
using ParallelProgrammingCsharp._6.ParallelLinq;

namespace ParallelProgrammingCsharp;

public class Program
{
    private static void Main(string[] args)
    {
        // Uncomment line of code you want to run.

        // DeadLockExample.Run();

        // Task programming.
        // CreateAndRunTaskExample.Run();
        // TaskCancellationExample.Run();
        // LinkedTokenSourceExample.Run();
        // WaitForATaskCompletionExample.Run(); // Need to uncomment one way to wait in method.
        // TaskExceptionHandlingExample.Run();
        // TaskAggregateExceptionHandleExample.Run();

        // Data sharing and synchronization.
        // CriticalSectionLockExample.Run();
        // InterlockedExample.Run();
        // SingleMutexExample.Run();
        // MultipleMutexExample.Run();
        // ReaderWriterLockExample.Run();

        // Concurrent collections.
        // ConcurrentDictionaryExample.Run();
        // ConcurrentQueueExample.Run();
        // ConcurrentStackExample.Run();
        // ConcurrentBagExample.Run();
        // BlockingCollectionExample.Run();

        // Task coordination.
        // ContinuationsExample.Run();
        // ChildTaskExample.Run(); // Have option to test different scenarios inside.
        // BarrierExample.Run();
        // CountdownEventExample.Run();
        // ResetEventExample.Run();
        // SemaphoreSlimExample.Run();

        // Parallel loops.
        // ParallelInvokeForForeachExample.Run();
        // BreakingCancellationsExceptionsExample.Run();
        // ThreadLocalStorageExample.Run();
        // PartitioningExample.Run();

        // Parallel Linq.
        // AsParallelAndParallelQueryExample.Run();
    }
}