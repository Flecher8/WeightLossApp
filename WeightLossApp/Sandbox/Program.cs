using System;

namespace Sandbox
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var ingr = new IngridientsDataVM();
            Console.WriteLine(ingr.result);
            Console.ReadLine();
        }
    }
}
