using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Mobile.Helpers;
using Mobile.Models;
using Mobile.Services;
using Xamarin.Forms;

namespace Mobile.ViewModels
{
    public class AddMealVM : PropertyChangedIpmlementator
    {
        // services
        private readonly IngredientMealService _mealService;

        // data fields
        private ObservableCollection<IngridientMeal> _mealIngridients;
        private string _mealName;

        // commands properties
        public Command CreateMeal;
        public Command AddIngredient => new Command(OnAddClick);
        public Command<IngridientMeal> RemoveIngredientCommand => new Command<IngridientMeal>(OnRemoveClick);

        public AddMealVM()
        {
            _mealService = new IngredientMealService();
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

        private void OnAddClick()
        {
            foreach (var ingridientMeal in _mealService.ConvertIngredientDataToMealParts(new List<IngridientData>()))
            {
                _mealIngridients.Add(ingridientMeal);
            }
        }

        private void OnRemoveClick(object obj)
        {
            var ingridientMeal = obj as IngridientMeal;
            _mealIngridients.Remove(ingridientMeal);
        }
    }
}
