using System;

namespace Sandbox
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var c = new TestVM { Name = "aasd"};

            Console.WriteLine(c.Name);
        }
    }
}
