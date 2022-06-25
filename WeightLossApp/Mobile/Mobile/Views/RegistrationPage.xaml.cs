using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DevExpress.XamarinForms.Editors;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : ContentPage, INotifyPropertyChanged
    {
        private ObservableCollection<string> values;

        public List<string> Items { get; set; }

        public ObservableCollection<string> Values
        {
            get => values;
            set
            {
                values = value;
                OnPropertyChanged();
            }
        }

        public RegistrationPage()
        {
            Items = new List<string>()
            {
                "alcohol-free",
                "celery-free",
                "crustacean-free",
                "dairy-free",
                "egg-free",
                "fish-free",
                "fodmap-free",
                "gluten-free",
                "immuno-supportive",
                "keto-friendly",
                "kidney-friendly",
                "kosher",
                "low-fat-abs",
                "low-potassium",
                "low-sugar",
                "luoine-free",
                "mustard-free",
                "no-oil-added",
                "paleo",
                "peanut-free",
                "pescatarian",
                "pork-free",
                "read-meat-free",
                "sesame-free",
                "soy-free",
                "sugar-conscious",
                "tree-nut-free",
                "vegan",
                "vegetarian",
                "wheat-free"
            };

            values = new ObservableCollection<string>();

            InitializeComponent();

            BindingContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            values.Clear();

            filterChipGroup.SelectedItems.Select(el => el.ToString()).ToList().ForEach(i => values.Add(i));

        }
    }
}