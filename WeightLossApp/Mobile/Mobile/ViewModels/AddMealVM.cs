using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Mobile.Helpers;
using Mobile.Models;
using Xamarin.Forms;

namespace Mobile.ViewModels
{
    public class AddMealVM : PropertyChangedIpmlementator
    {
        // data fields
        private ObservableCollection<IngridientMeal> _mealIngridients;
        private string _mealName;

        // commands properties
        public Command CreateMeal; 
        public Command AddIngredient => new Command(OnAddClick);
        public Command<IngridientMeal> RemoveIngredientCommand => new Command<IngridientMeal>(OnRemoveClick);

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

        }

        private void OnRemoveClick(object obj)
        {
            var ingridientMeal = obj as IngridientMeal;
            _mealIngridients.Remove(ingridientMeal);
        }
    }
}
