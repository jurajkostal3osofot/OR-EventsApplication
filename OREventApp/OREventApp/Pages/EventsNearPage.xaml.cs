using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OREventApp.Helpers;
using OREventApp.Models;
using OREventApp.Utilities;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OREventApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EventsNearPage : ContentPage
	{
		public EventsNearPage ()
		{
			InitializeComponent ();
		    if (Connection.CheckInternetConnection())
		        LoadEventsToListView();
		    else
		        Connection.ShowNotificationNoInternetConnection();
        }

	    private async void LoadEventsToListView()
	    {
	        EventHelper helper = new EventHelper();
	        var events = await helper.GetEventsAsync();

	        ObservableCollection<CellModel> listOfCollection = new ObservableCollection<CellModel>();
	        ActivityList.ItemsSource = listOfCollection;

	        foreach (var loadedEvent in events)
	        {
	            listOfCollection.Add(new CellModel()
	            {
	                Heading = loadedEvent.EventType + " at " + loadedEvent.Date.ToString("HH':'mm"),

	            });
	        }


	    }
    }
}