using System;
using System.Linq;
using System.Reflection;
using OREventApp.Domain;
using OREventApp.Helpers;
using OREventApp.Utilities;
using Plugin.Geolocator.Abstractions;
using Shared.Enums;
using Shared.Models;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using Position = Xamarin.Forms.Maps.Position;
using PositionEventArgs = OREventApp.Renderers.PositionEventArgs;

namespace OREventApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddEventPage : ContentPage
    {
        public Plugin.Geolocator.Abstractions.Position MyPosition;
        private Geocoder geoCoder;
        public AddEventPage()
        {
            InitializeComponent();
            InitVarsAndPosition();

        }

        public async void InitVarsAndPosition()
        {
            var activityListy = Enum.GetNames(typeof(EventTypeShared)).ToList();
            ActivityPicker.ItemsSource = activityListy;
            DatePicker.Date = DateTime.Now;
            TimePicker.Time = DateTime.Now.TimeOfDay;
            geoCoder = new Geocoder();
            MyPosition = await LocationHelper.GetCurrentLocation();
            MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(MyPosition.Latitude, MyPosition.Longitude), Distance.FromKilometers(2)));
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
                            EventType = (EventTypeShared) ActivityPicker.SelectedIndex
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
            if (ActivityPicker.SelectedIndex != -1) return true;
            return false;
        }

        private void HandleOnPositionChanged(object sender, PositionEventArgs e)
        {
            ObtainAddress(e.Position);
        }

        private async void ObtainAddress(Position e)
        {
            var position = new Position(e.Latitude, e.Longitude);
            var possibleAddresses = await geoCoder.GetAddressesForPositionAsync(position);
            //Address.Text = possibleAddresses.FirstOrDefault();
            try
            {
                string[] strArr = possibleAddresses.FirstOrDefault().Split(',');
                Address.Text = strArr[0] ?? "No Address";
                if (strArr[0] == "Unnamed Road")
                    Address.Text = strArr[3];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Address.Text = "No Address";
            }
            

        }
    }
}