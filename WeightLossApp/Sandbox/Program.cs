using System;

namespace Sandbox
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var ingr = new AppProfile();
            ingr.LoadAsyncPM();
            Console.ReadLine();
        }
    }
}
