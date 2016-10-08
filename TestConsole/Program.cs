using System;
using System.IO;
using PowerQualityModel;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var text = File.ReadAllText($"d:\\1.txt");
            var cfg = new PowerConfig(text);
            Console.WriteLine(cfg["name"]);
            Console.ReadKey();
        }
    }
}
