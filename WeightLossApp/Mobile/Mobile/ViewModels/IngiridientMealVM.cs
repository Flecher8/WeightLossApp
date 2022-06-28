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

namespace Mobile.ViewModels
{
    public class IngiridientMealVM : PropertyChangedIpmlementator
    {
        // services
        private readonly IngiridentMealService _mealIngridientsService;
        private readonly MealService _mealService;

        // data fields
        private ObservableCollection<IngridientMeal> _mealIngridients;
        private string _mealName;

        // commands properties
        public Command CreateMeal => new Command(OnMealCreate);
        public Command<IngridientMeal> EditMealIngridients => new Command<IngridientMeal>(OnEditClick);
        public Command AddIngredient => new Command(OnAddIngiridientClick);
        public Command<IngridientMeal> RemoveIngredient => new Command<IngridientMeal>(OnRemoveIngridientClick);

        public IngiridientMealVM()
        {
            _mealService = new MealService();
            _mealIngridientsService = new IngiridentMealService();
            _mealIngridients = new ObservableCollection<IngridientMeal>();
        }

        // data properties
        public ObservableCollection<IngridientMeal> MealIngridients
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
            get => _mealName;
            set
            {
                _mealName = value;
                OnPropertyChanged();
            }
        }

        private async void OnMealCreate()
        {
            await _mealService.PostAsync(MealName);
            await _mealIngridientsService.PostAsync(_mealIngridients);
        }

        private async void OnEditClick(object obj)
        {
            var ingridientMeal = obj as IngridientMeal;
            await _mealIngridientsService.PutAsync(ingridientMeal);
        }

        private void OnAddIngiridientClick()
        {
            IEnumerable<IngridientMeal> filtered = _mealIngridientsService.GetOnlyUnknownIngredients(MealIngridients,
                _mealIngridientsService.ConvertIngredientDataToMealParts(new List<IngridientData>()));
            foreach (var ingridientMeal in filtered)
            {
                _mealIngridients.Add(ingridientMeal);
            }
        }

        private async void OnRemoveIngridientClick(object obj)
        {
            var ingridientMeal = obj as IngridientMeal;
            _mealIngridients.Remove(ingridientMeal);
            await _mealIngridientsService.DeleteAsync(ingridientMeal);
        }
    }
}
