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
            string login = Xamarin.Essentials.Preferences.Get("UserLogin", "empty");

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
