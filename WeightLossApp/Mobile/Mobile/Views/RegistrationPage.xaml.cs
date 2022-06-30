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
using Mobile.ViewModels;

namespace Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : ContentPage, INotifyPropertyChanged
    {
        public RegistrationVM ViewModel { get; set; }



        public RegistrationPage()
        {
            
            InitializeComponent();

            ViewModel = new RegistrationVM(filterChipGroup.SelectedItems);

            BindingContext = ViewModel;
        }

    }
}