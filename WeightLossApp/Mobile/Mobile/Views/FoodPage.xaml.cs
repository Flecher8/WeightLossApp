using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Mobile.ViewModels;
using System.Globalization;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FoodPage : ContentPage, INotifyPropertyChanged
    {
        public FoodPage()
        {
            InitializeComponent();

            BindingContext = this;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MealsPage.Refresh();
        }

    }

}