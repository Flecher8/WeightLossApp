using Mobile.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Mobile.ViewModels;

namespace Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MealAddPage : ContentPage
    {
        private ObservableCollection<Meal> _parentsMeals;
        public IngiridientMealVM ViewModel { get; set; }

        public MealAddPage(ObservableCollection<Meal> parentsMeals)
        {
            InitializeComponent();
            _parentsMeals = parentsMeals;
            ViewModel = new IngiridientMealVM(_parentsMeals);
            load();
        }

        private async void load()
        {
            await ViewModel.InitialLoad();
            BindingContext = ViewModel;
        }

    }
}