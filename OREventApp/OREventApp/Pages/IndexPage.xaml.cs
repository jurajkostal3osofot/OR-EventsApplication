using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OREventApp.Helpers;
using OREventApp.Renderers;
using Shared.Models;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace OREventApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class IndexPage : ContentPage
	{
	    private IndexMap _map;
		public IndexPage ()
		{
			InitializeComponent ();
            InnitMap();
            LoadPins();
		}

	    public void InnitMap()
	    {
	        _map = new IndexMap(
	            MapSpan.FromCenterAndRadius(
	                new Position(37, -122), Distance.FromMiles(0.3)))
	        {
	            IsShowingUser = true,
	            HeightRequest = 320,
	            WidthRequest = 200,
	            VerticalOptions = LayoutOptions.FillAndExpand
	        };
	        var stack = new StackLayout { Spacing = 0 };
	        stack.Children.Add(_map);
	        Content = stack;
        }

	    private async void LoadPins()
	    {
	        EventHelper helper = new EventHelper();
	        IEnumerable<EventShared> events = await helper.GetEventsAsync();

	        foreach (var loadedEvent in events)
	        {
	            var pin = new Pin
	            {
	                Type = PinType.Place,
	                Position = new Position(loadedEvent.Latitude.GetValueOrDefault(),loadedEvent.Longitude.GetValueOrDefault()),
	                Label = ""
	            };
                _map.Pins.Add(pin);
            }
	    }
	}
}