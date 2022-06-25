using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Mobile.ViewModels;

namespace Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FoodPage : ContentPage
    {
        public IngridientDataVM ViewModel { get; set; }

        public FoodPage()
        {
            InitializeComponent();

            ViewModel = new IngridientDataVM();

            BindingContext = ViewModel;
        }
    }
}