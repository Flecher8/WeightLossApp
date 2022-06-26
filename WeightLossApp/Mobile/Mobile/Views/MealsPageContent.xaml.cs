using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Mobile.Models;

namespace Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MealsPageContent : ContentView
    {
        public List<Meal> Meals { get; set; }

        public MealsPageContent()
        {
            InitializeComponent();

            Meals = new List<Meal>()
            {
                new Meal()
                {
                    Name = "Dinner",

                },
                new Meal()
                {
                    Name = "Lunch",

                }
            };


            BindingContext = this;
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }
    }

    
}