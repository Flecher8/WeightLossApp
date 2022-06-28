using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Mobile.Helpers;
using Mobile.Models;
using Mobile.Services;

namespace Mobile.ViewModels
{
    public class MealMenuVM : PropertyChangedIpmlementator
    {
        // services
        private readonly MealService _mealService;

        // data
        private ObservableCollection<Meal> _meals;

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
    }
}
