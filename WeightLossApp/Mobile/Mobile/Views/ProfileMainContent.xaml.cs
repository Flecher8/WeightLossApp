using Mobile.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfileMainContent : ContentView
    {

        private AppProfile profile = AppProfile.Instance;

        public string Login
        {
            get { return profile.Login; }
        }

        public DateTime BirthDate
        {
            get { return (DateTime)profile.Member.Birthday; }
        }

        public double Weight
        {
            get { return Math.Round(profile.Member.Weight, 2);}
        }

        public double Height_
        {
            get { return Math.Round(profile.Member.Height, 2); }
        }

        public int Age
        {
            get { return (profile.Member.Birthday - DateTime.Now).Days / -365; }
        }

        public string Gender
        {
            get { return profile.Member.Gender; }
        }

        public float LevelProgress
        {
            get { return (profile.Profile.Exp % 1000) / 1000; }
        }

        public string LevelText
        {
            get { return "Level " + profile.GetLevel(); }
        }


        public ProfileMainContent()
        {
            InitializeComponent();
        }

        private void LogOut_Clicked(object sender, EventArgs e)
        {
            profile.LogOut();
            App.Current.MainPage = new LoginPage();
        }
    }
}