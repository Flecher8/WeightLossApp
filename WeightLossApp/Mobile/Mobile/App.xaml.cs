using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            DevExpress.XamarinForms.Navigation.Initializer.Init();
            DevExpress.XamarinForms.Charts.Initializer.Init();

            MainPage = new Views.LoginPage();
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
