using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Mobile.Services;
using System.Threading.Tasks;

namespace Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            DevExpress.XamarinForms.Navigation.Initializer.Init();
            DevExpress.XamarinForms.Charts.Initializer.Init();
            DevExpress.XamarinForms.Scheduler.Initializer.Init();
            string login = Xamarin.Essentials.Preferences.Get("UserLogin", "empty");

            if (login != "empty" && login != "")
            {
                AppProfile profile = AppProfile.Instance;

                var navigationPage = new NavigationPage(new MainPage());

                MainPage = navigationPage;

            }
            else
            {
                MainPage = new Views.LoginPage();
            }




        }

        public static async Task LoadProfile(string login)
        {
            AppProfile profile = AppProfile.Instance;

            await profile.LoadAsyncPM(login);
            await profile.LoadAsync(profile.Profile.Id);
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }


    }
}
