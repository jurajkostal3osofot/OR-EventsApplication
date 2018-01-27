using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OREventApp.Helpers;
using OREventApp.Utilities;
using Shared.Enums;
using Shared.Models;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace OREventApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddEventPage : ContentPage
	{
		public AddEventPage ()
		{
			InitializeComponent ();
		    var activityListy = Enum.GetNames(typeof(EventTypeShared)).ToList();
		    PlacePicker.ItemsSource = activityListy;
        }

	    private async void AddNewEvent(object sender, EventArgs e)
	    {
	        if (Connection.CheckInternetConnection())
	        {
	            if (CheckPinExist())
	            {
	                Pin pin = MyMap.Pins[0];
	                EventHelper helper = new EventHelper();
	                EventShared newEvent = new EventShared
	                {
	                    Date = DatePicker.Date,
                        Latitude = pin.Position.Latitude,
                        Longitude = pin.Position.Longitude,
                        EventType = (EventTypeShared)PlacePicker.SelectedIndex
	                };
                    var result = await helper.SaveEventAsync(newEvent);
                    if (result)
                    {
                        await DisplayAlert("Success!", "Your event has been created.", "Got ya!");
                        new IndexPage();
                    }
                    else
                    {
                        await DisplayAlert("Fail!", "Your event has NOT been created. Blame the IT department", "Fine then");
                    }
	                
                    
                    
                }   
	            else
	            {
	                Connection.ShowNotificationNoMarkerSelected();
                }
	        }
	        else
	        {
	            Connection.ShowNotificationNoInternetConnection();
	        }
        }

	    private bool CheckPinExist()
	    {
	        if (MyMap.Pins.Count > 0)
	        {
	            return true;
	        }
	        return false;
	    }
	}
}