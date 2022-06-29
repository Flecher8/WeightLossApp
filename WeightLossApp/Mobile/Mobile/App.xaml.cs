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
            string login = Xamarin.Essentials.Preferences.Get("UerLogin", "empty");

            if (login != "empty")
            {
                AppProfile profile = AppProfile.Instance;

                LoadProfile(login);

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
            profile.LoadAsync(profile.Profile.Id).Wait(1000);
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
