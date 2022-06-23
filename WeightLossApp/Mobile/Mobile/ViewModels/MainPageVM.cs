using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Mobile.Helpers;
using Xamarin.Forms;

namespace Mobile.ViewModels
{
    public class MainPageVM : PropertyChangedIpmlementator
    {
        private ObservableCollection<DonutChartPiece> mainChartValues;
        private float nutritionProgress;
        private float sportProgress;

        // Property for PieChart, should contain 2 values 
        // First Progress in %
        // Second 100% - Progress
        public ObservableCollection<DonutChartPiece> MainChartValues 
        { 
            get => mainChartValues;
            set
            {
                mainChartValues = value;
                OnPropertyChanged();
            }
        }

        // Progress by nutrition
        public float NutritionProgress
        {
            get => nutritionProgress;
            set
            {
                nutritionProgress = value;
                OnPropertyChanged();
            }
        }

        public float SportProgress
        {
            get => sportProgress;
            set
            {
                sportProgress = value;
                OnPropertyChanged();
            }
        }

        // Color palette for chart
        public Color[] Palette { get; set; }

        public MainPageVM()
        {
            // This initialization should be replaced by calculated values
            MainChartValues = new ObservableCollection<DonutChartPiece> 
            {
                new DonutChartPiece("Progress", 62),
                new DonutChartPiece("Empty", 38)
            };

            NutritionProgress = 0.45f;
            SportProgress = 0.60f;

            // Initialization of color Palette for chart
            Palette = new Color[2];
            Palette[0] = (Color)Application.Current.Resources["PrimaryColor"];
            Palette[1] = (Color)Application.Current.Resources["BackgroundColor"];
        }
    }
}
