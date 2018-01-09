using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OREventApp.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace OREventApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class IndexPage : ContentPage
	{
		public IndexPage ()
		{
			InitializeComponent ();
            InnitMap();
		}

	    public void InnitMap()
	    {
	        var map = new IndexMap(
	            MapSpan.FromCenterAndRadius(
	                new Position(37, -122), Distance.FromMiles(0.3)))
	        {
	            IsShowingUser = true,
	            HeightRequest = 320,
	            WidthRequest = 200,
	            VerticalOptions = LayoutOptions.FillAndExpand
	        };
	        var stack = new StackLayout { Spacing = 0 };
	        stack.Children.Add(map);
	        Content = stack;

        }
	}
}