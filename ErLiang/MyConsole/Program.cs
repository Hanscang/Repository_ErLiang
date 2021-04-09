using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace MyConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            HashSet<string> s = new HashSet<string>();
            s.Add("12");
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("a", "21");


        } 

        private static void threadtest()
        {
            Console.WriteLine($"{Thread.GetCurrentProcessorId()}");
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId.ToString());
            Console.WriteLine(Thread.CurrentThread.IsBackground);
            Console.WriteLine("before");
            Thread thread1 = new Thread(Print);
            Thread thread2 = new Thread(Print);
            thread1.Start();
            thread2.Start();
        }

        private static void Print()
        {

            for (int i = 1; i < 50; ++i)
            {
                Console.WriteLine(Thread.CurrentThread.IsBackground);
                Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}    {Thread.GetCurrentProcessorId()}: {i}");
            }
        }
    }
}
