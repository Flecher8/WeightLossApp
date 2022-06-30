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
	public partial class MainPageContent : ContentPage
	{

		public MainPageVM ViewModel { get; set; }

		public MainPageContent ()
		{
			ViewModel = new MainPageVM();
			InitializeComponent ();

			BindingContext = ViewModel;
		}
	}
}