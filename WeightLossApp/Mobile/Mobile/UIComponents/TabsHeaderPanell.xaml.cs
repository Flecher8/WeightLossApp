using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Mobile.Views;

namespace Mobile.UIComponents
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabsHeaderPanell : ContentView
    {
        public string PanellTitle
        {
            get => (string)GetValue(PanellTitleProperty);
            set => SetValue(PanellTitleProperty, value);
        }

        public static readonly BindableProperty PanellTitleProperty = BindableProperty.Create(
            "PanellTitle",        // the name of the bindable property
            typeof(string),     // the bindable property type
            typeof(TabsHeaderPanell),   // the parent object type
            "Title");      // the default value for the property


        public TabsHeaderPanell()
        {
            InitializeComponent();
        }

        private void Profile_Clicked(object sender, EventArgs e)
        {
            ProfilePage page = new ProfilePage();
            Navigation.PushAsync(page);
        }
    }
}