using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DevExpress.Mvvm.Native;
using Mobile.Helpers;
using Mobile.Models;
using Mobile.Services;
using Xamarin.Forms;
using Mobile.Views;

namespace Mobile.ViewModels
{
    public class IngiridientMealVM : PropertyChangedIpmlementator
    {
        // services
        private IngiridentMealService _mealIngridientsService;
        private MealService _mealService;

        // data fields
        private ObservableCollection<Food> _mealIngridients;
        private Meal _meal;
        private ObservableCollection<Meal> _parentMeals;

        // commands properties
        public Command CreateMeal => new Command(OnMealCreate);
        public Command<IngridientMeal> EditMealIngridients => new Command<IngridientMeal>(OnEditClick);
        public Command AddIngredient => new Command(OnAddIngiridientClick);
        public Command<IngridientMeal> RemoveIngredient => new Command<IngridientMeal>(OnRemoveIngridientClick);

        public IngiridientMealVM(ObservableCollection<Meal> meals)
        {
            _mealService = new MealService();
            _mealIngridients = new ObservableCollection<Food>();
            _parentMeals = meals;
            _meal = new Meal() { Name = "TempName" };
            _mealIngridientsService = new IngiridentMealService(_meal);

        }

        public async Task InitialLoad()
        {

            
        }

        // data properties
        public ObservableCollection<Food> MealIngridients
        {
            get => _mealIngridients;
            set
            {
                _mealIngridients = value;
                OnPropertyChanged();
            }
        }

        public string MealName
        {
            get => _meal.Name;
            set
            {
                _meal.Name = value;
                OnPropertyChanged();
            }
        }

        private async void OnMealCreate()
        {
            await _mealService.PostAsync(_meal);
            await _mealIngridientsService.PostAsync(_mealIngridients);
            await _mealIngridientsService.PostDAMAsync();

            _parentMeals.Add(_meal);
        }

        private async void OnEditClick(object obj)
        {
            var ingridientMeal = obj as IngridientMeal;
            await _mealIngridientsService.PutAsync(ingridientMeal);
        }

        private void OnAddIngiridientClick()
        {
            App.Current.MainPage.Navigation.PushAsync(new IngridientsPage(MealIngridients));
        }

        private async void OnRemoveIngridientClick(object obj)
        {
            var ingridientMeal = obj as Food;
            _mealIngridients.Remove(ingridientMeal);
        }
    }
}
