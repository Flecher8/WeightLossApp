using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Mobile.ViewModels;

namespace Mobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NutrientsStateView : ContentView
	{
		NutrientsStateVM ViewModel { get; set; }

		public NutrientsStateView ()
		{
			ViewModel = new NutrientsStateVM ();
			InitializeComponent ();

			BindingContext = ViewModel;
		}
	}
}