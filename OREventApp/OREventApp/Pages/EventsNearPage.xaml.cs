using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlTypes;
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
		    {
                Connection.ShowNotificationNoInternetConnection();
            }
		        
        }

	    private async void LoadEventsToListView()
	    {
	        EventHelper helper = new EventHelper();
	        var events = await helper.GetEventsAsync();

            if (events == null)
            {
                Connection.ShowNotificationServerNotReachable();
            }
            else
            {
                ObservableCollection<CellModel> listOfCollection = new ObservableCollection<CellModel>();

                ActivityList.IsPullToRefreshEnabled = true;
                ActivityList.ItemSelected += (sender, e) => {
                    ((ListView)sender).SelectedItem = null;
                };


                foreach (var loadedEvent in events)
                {
                    listOfCollection.Add(new CellModel()
                    {
                        Heading = loadedEvent.EventType + " at " + loadedEvent.Date.ToString("HH':'mm"),
                        MiniMap = "ic_launcher.png"

                    });
                }
                ActivityList.ItemsSource = listOfCollection;
            }

	        

            
        }
    }

 
}