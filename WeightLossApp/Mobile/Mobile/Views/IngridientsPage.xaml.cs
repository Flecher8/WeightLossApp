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
    public partial class IngridientsPage : ContentPage
    {

        public IngridientsDataVM ViewModel { get; set; }

        public IngridientsPage()
        {
            ViewModel = new IngridientsDataVM(this);
            InitializeComponent();
            BindingContext = ViewModel;
        }
    }
}