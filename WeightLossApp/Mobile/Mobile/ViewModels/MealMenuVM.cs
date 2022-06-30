using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Mobile.Helpers;
using Mobile.Models;
using Mobile.Services;
using Xamarin.Forms;
using Mobile.Views;
using System.Linq;
using System.Threading.Tasks;

namespace Mobile.ViewModels
{
    public class MealMenuVM : PropertyChangedIpmlementator
    {
        // services
        private readonly MealService _mealService;

        // data
        private ObservableCollection<Meal> _meals;

        // commands
        public Command CreateMealCommand { get; set; }
        public Command<Meal> EditMealCommand { get; set; }
        public Command DeleteMealCommand { get; set; }

        public MealMenuVM()
        {
            _mealService = new MealService();
            CreateMealCommand = new Command(() => OnAdd());
            DeleteMealCommand = new Command((o) => OnRemoveClick(o));
            Meals = new ObservableCollection<Meal>();
        }

        public async Task Initialize()
        {
            await _mealService.GetAsync();

            Meals.Clear();

            foreach (Meal m in _mealService.MealRepository)
            {
                Meals.Add(m);
            }
        }

        public ObservableCollection<Meal> Meals
        {
            get => _meals;
            set
            {
                _meals = value;
                OnPropertyChanged();
            }
        }

        private async void OnRemoveClick(object obj)
        {
            int id = int.Parse(obj.ToString());
            Meal meal = _meals.Where(x => x.Id == id).FirstOrDefault();
            _meals.Remove(meal);
            await _mealService.DeleteAsync(meal);

        }

        private void OnAdd()
        {
            App.Current.MainPage.Navigation.PushAsync(new MealAddPage(_meals));
        }
    }
}
