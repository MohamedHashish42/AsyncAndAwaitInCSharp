using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAndAwaitInCSharp
{
    class kitchen
    {

        public static void SynchronousBreakfast()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            Coffee cup = SyncBreakfast.PourCoffee();
            Console.WriteLine("coffee is ready");

            Toast toast = SyncBreakfast.ToastBread(2);
            SyncBreakfast.ApplyButter(toast);
            SyncBreakfast.ApplyJam(toast);
            Console.WriteLine("toast is ready");

            Egg eggs = SyncBreakfast.FryEggs(2);
            Console.WriteLine("eggs are ready");


            Console.WriteLine("Breakfast is ready!");
            Console.WriteLine($"Synchronous breakfasttime is {stopwatch.Elapsed} \n \n \n"); //9 Seconds
        }

        public static async Task AsynchronousBreakfast()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            Coffee cup = AsyncBreakfast.PourCoffee();
            Console.WriteLine("coffee is ready");
            /*
            Here we start preparing toast task and egg task at the same time and this allows us to make 
            the breakfast faster because instead Of waiting 3 seconds until toast are made then 6 seconds 
            until eggs are made (total will be about 9 Sec), we take about 6 seconds for both  
            */
            Task<Toast> toastTask = AsyncBreakfast.ToastBreadAsync(2);
            Task<Egg> eggTask = AsyncBreakfast.FryEggsAsync(2);

            Toast toast = await toastTask; //Here we wait until we get the toast 
            AsyncBreakfast.ApplyButter(toast);
            AsyncBreakfast.ApplyJam(toast);
            Console.WriteLine("toast is ready");

            Egg eggs = await eggTask;      //here we wait until we get the eggs 
            Console.WriteLine("eggs are ready");

            Console.WriteLine("Breakfast is ready!");
            Console.WriteLine($"Asynchronous breakfasttime is {stopwatch.Elapsed} \n \n \n"); //6 seconds
        }
        public static async Task AsynchronousBreakfastBetterWay()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            Coffee cup = AsyncBreakfast.PourCoffee();
            Console.WriteLine("coffee is ready");

            Task<Toast> toastTask = AsyncBreakfast.ToastBreadAsync(2);
            Task<Egg> eggTask = AsyncBreakfast.FryEggsAsync(2);
            var breakfastTasks = new List<Task> { eggTask, toastTask};
            while (breakfastTasks.Count > 0)
            {
                Task finishedTask = await Task.WhenAny(breakfastTasks);
                if (finishedTask == eggTask)
                {
                    Console.WriteLine("eggs are ready");
                }
          
                else if (finishedTask == toastTask)
                {
                    AsyncBreakfast.ApplyButter( await toastTask);
                    AsyncBreakfast.ApplyJam(await toastTask);
                    Console.WriteLine("toast is ready");
                }
                breakfastTasks.Remove(finishedTask);
              
            }
            Console.WriteLine($"Asynchronous breakfasttime is {stopwatch.Elapsed} \n \n \n"); //6 seconds
        }

    }
}
