using System;
using System.Threading;
using Mobile.Services;
namespace Sandbox
{
    internal class Program
    {

        static void Main(string[] args)
        {
            var loginVM = new LoginVM();
            var appProfile = AppProfile.Instance;

            LoadProfile(appProfile);
            
            Console.WriteLine(123);
   
            Console.ReadLine();
        }
        private static async void  LoadProfile(AppProfile appProfile)
        {
            await appProfile.LoadAsyncPM("Val");
            await appProfile.LoadAsync(1);
            var mainPageVM = new MainPageVM();
            await mainPageVM.LoadAsync();
            mainPageVM.initialize(appProfile);
            
            Console.WriteLine(mainPageVM.NutritionProgress);
        }
    }
}
