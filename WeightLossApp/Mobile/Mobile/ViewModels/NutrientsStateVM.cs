using System;
using System.Collections.Generic;
using System.Text;
using Mobile.Helpers;

namespace Mobile.ViewModels
{
    class NutrientsStateVM : PropertyChangedIpmlementator
    {
        // Is this tab selected
        private bool isSelected;

        // Values for progress bars
        private float calories;
        private float proteines;
        private float carbs;
        private float fats;

        // Here should be Profile, to take data from it
        // and to get disired values on each creteria   

        // ... Profile ... 

        public float Calories
        {
            get { return calories; }
            set
            {
                calories = value;
                OnPropertyChanged();
            }
        }

        public float Proteines
        {
            get { return proteines; }
            set
            {
                proteines = value;
                OnPropertyChanged();
            }
        }

        public float Carbs
        {
            get { return carbs; }
            set
            {
                carbs = value;
                OnPropertyChanged();
            }
        }

        public float Fats
        {
            get { return fats; }
            set
            {
                fats = value;
                OnPropertyChanged();
            }
        }

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                if (value == isSelected) return;
                isSelected = value;
                OnPropertyChanged();
            }
        }

        public NutrientsStateVM()
        {
            isSelected = false;
            Calories = 0.44f;
            Proteines = 0.23f;
            Carbs = 0.68f;
            Fats = 1f;
        }
    }
}
