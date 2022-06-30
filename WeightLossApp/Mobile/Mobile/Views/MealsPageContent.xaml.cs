using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Mobile.Models;
using Mobile.ViewModels;

namespace Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MealsPageContent : ContentView
    {
        //public List<Meal> Meals { get; set; }
        public MealMenuVM ViewModel { get; set; }

        public MealsPageContent()
        {
            InitializeComponent();
            ViewModel = new MealMenuVM();

            BindingContext = ViewModel;
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }

        private void RefreshView_Refreshing(object sender, EventArgs e)
        {
            Refresh();
        }

        public async Task Refresh()
        {
            await ViewModel.Initialize();
            this.RefreshView.IsRefreshing = false;
        }
    }
}