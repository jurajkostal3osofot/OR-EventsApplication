using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Naxam.Controls.Forms;
using Naxam.Controls.Platform.Droid;
using OREventApp.Interfaces;
using OREventApp.Pages;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

namespace OREventApp
{
	public partial class MainPage : BottomTabbedPage
	{
		public MainPage()
		{
		    InitializeComponent();
		    BottomTabbedRenderer.IconSize = 24;
		    BottomTabbedRenderer.ItemPadding = new Thickness(6);
		    BottomTabbedRenderer.BottomBarHeight = 56;
		    BottomTabbedRenderer.ItemSpacing = 12;

		    ToolbarItems.Add(new ToolbarItem("Action Name", "ic_add_white_24dp", async () =>
		    {
		        await Navigation.PushAsync(new AddEventPage());
		    }));

        }
	}
}
