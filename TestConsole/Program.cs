using System;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(DateTime.MinValue.ToString("yyyy-MM-dd HH:mm:ss fff"));
            Console.WriteLine(DateTime.MinValue.Ticks);
            Console.WriteLine(DateTime.MaxValue.ToString("yyyy-MM-dd HH:mm:ss fff"));
            Console.WriteLine(DateTime.MaxValue.Ticks);
            Console.WriteLine((new TimeSpan(10000)).TotalMilliseconds);
            Console.ReadKey();
        }
    }
}
