using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OREventApp.Interfaces;
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

	    private void IndexClicked(object sender, EventArgs e)
	    {
	        var page = new NavigationPage(new IndexPage());
	        Navigation.PushAsync(page);
	    }

	    private void AddEventClicked(object sender, EventArgs e)
	    {
	        var page = new NavigationPage(new AddEventPage());
	        Navigation.PushAsync(page);
        }
	}
}
