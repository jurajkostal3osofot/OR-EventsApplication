using System;
using System.Linq;
using OREventApp.Helpers;
using OREventApp.Utilities;
using Shared.Enums;
using Shared.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OREventApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddEventPage : ContentPage
    {
        public AddEventPage()
        {
            InitializeComponent();
            var activityListy = Enum.GetNames(typeof(EventTypeShared)).ToList();
            PlacePicker.ItemsSource = activityListy;
        }

        private async void AddNewEvent(object sender, EventArgs e)
        {
            if (Connection.CheckInternetConnection())
                if (CheckPinExist())
                    if (CheckActivitySelected())
                    {
                        var pin = MyMap.Pins[0];
                        var helper = new EventHelper();
                        var newEvent = new EventShared
                        {
                            Date = DatePicker.Date,
                            Latitude = pin.Position.Latitude,
                            Longitude = pin.Position.Longitude,
                            EventType = (EventTypeShared) PlacePicker.SelectedIndex
                        };
                        var result = await helper.SaveEventAsync(newEvent);
                        if (result)
                        {
                            await DisplayAlert("Success!", "Your event has been created.", "Got ya!");
                            new IndexPage();
                        }
                        else
                        {
                            await DisplayAlert("Fail!", "Your event has NOT been created. Blame the IT department",
                                "Fine then");
                        }
                    }
                    else
                    {
                        Connection.ShowNotificationNoActivitySelected();
                    }
                else
                    Connection.ShowNotificationNoMarkerSelected();
            else
                Connection.ShowNotificationNoInternetConnection();
        }

        private bool CheckPinExist()
        {
            if (MyMap.Pins.Count > 0) return true;
            return false;
        }

        private bool CheckActivitySelected()
        {
            if (PlacePicker.SelectedIndex != -1) return true;
            return false;
        }
    }
}