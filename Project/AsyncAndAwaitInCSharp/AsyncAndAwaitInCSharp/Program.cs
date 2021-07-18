using System;
using System.Threading.Tasks;

namespace AsyncAndAwaitInCSharp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            kitchen.SynchronousBreakfast();
            await kitchen.AsynchronousBreakfast();
            await kitchen.AsynchronousBreakfastBetterWay();

        }
    }
}
