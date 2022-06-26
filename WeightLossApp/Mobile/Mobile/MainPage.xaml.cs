using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using DevExpress.XamarinForms.Navigation;
using Mobile.ViewModels;

namespace Mobile
{
    public partial class MainPage : TabPage
    {
        public MainPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            InitializeComponent();

        }
    }
}
