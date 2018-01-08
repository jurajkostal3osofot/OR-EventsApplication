using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OREventApp.Pages;
using Xamarin.Forms;

namespace OREventApp
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

	    private void Cell_OnTapped(object sender, EventArgs e)
	    {
	        var page = new EventPickerPage();
	        page.GetPickerEvents.ItemSelected += (src, args) =>
	        {
	            PickerEvents.Text = args.SelectedItem.ToString();

                Navigation.PopAsync();
	        };

	        Navigation.PushAsync(page);
        }
	}
}
