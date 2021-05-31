using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace ConsoleApp1
{
    class Program
    {
        static AutoResetEvent task1Handle = new AutoResetEvent(false);//wait state(red)
        static AutoResetEvent task2Handle = new AutoResetEvent(false);//wait state(red)
        static void Main(string[] args)
        {

            var t1 = Task.Factory.StartNew(() => PrintOddNumbers());
            var t2 = Task.Factory.StartNew(() => PrintEvenNumbers());
           // task1Handle.Set();
            Task.WaitAny(t1,t2);

            Console.WriteLine("End");
            Console.ReadLine();
        }

        static void PrintOddNumbers()
        {
            int[] arr = new int[] { 1, 3, 5, 7, 9, 11, 13, 15 };

            foreach (var item in arr)
            {
                Console.WriteLine(item);
                task2Handle.Set();
                task1Handle.WaitOne();
            }
            
        }

        static void PrintEvenNumbers()
        {
            int[] arr = new int[] { 2, 4, 6, 8, 10, 12, 14 };
            task2Handle.WaitOne();
            foreach (var item in arr)
            {
                Console.WriteLine(item);
                task1Handle.Set();
                task2Handle.WaitOne();
            }

        }
    }
}
