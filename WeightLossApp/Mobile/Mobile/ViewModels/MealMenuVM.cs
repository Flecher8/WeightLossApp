using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Mobile.Helpers;
using Mobile.Models;
using Mobile.Services;
using Xamarin.Forms;

namespace Mobile.ViewModels
{
    public class MealMenuVM : PropertyChangedIpmlementator
    {
        // services
        private readonly MealService _mealService;

        // data
        private ObservableCollection<Meal> _meals;

        // commands
        public Command CreateMealCommand;
        public Command<Meal> EditMealCommand;
        public Command<Meal> DeleteMealCommand;

        public MealMenuVM()
        {
            _mealService = new MealService();
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

        private void OnRemoveClick(object obj)
        {
            var meal = obj as Meal;
            _meals.Remove(meal);
        }
    }
}
