using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Mobile.ViewModels;
using System.Globalization;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FoodPage : ContentPage, INotifyPropertyChanged
    {
        private int selectedIndex;
        public bool firstSelected;
        public bool secondSelected;

        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                if (selectedIndex == value) return;
                else if (value == 0)
                {
                    firstSelected = true;
                }
                else if (value == 1)
                {
                    secondSelected = true;
                }

                selectedIndex = value;
                OnPropertyChanged();
            }
        }

        public bool FirstSelected
        {
            get => firstSelected;
            set
            {
                firstSelected = value;
                OnPropertyChanged();
            }
        }

        public bool SecondSelected
        {
            get => secondSelected;
            set
            {
                secondSelected = value;
                OnPropertyChanged();
            }
        }


        public FoodPage()
        {
            InitializeComponent();

            BindingContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    class IsSelectedToColorConverter : IValueConverter
    {
        public Color DefaultColor { get; set; }
        public Color SelectedColor { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool boolValue)) return DefaultColor;
            return (boolValue) ? SelectedColor : DefaultColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}