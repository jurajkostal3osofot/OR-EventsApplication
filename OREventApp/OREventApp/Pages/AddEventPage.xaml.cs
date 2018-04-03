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
        public Position _selectedPosition;
        private Geocoder geoCoder;
        public AddEventPage()
        {
            InitializeComponent();
            InitVarsAndPosition();

        }

        public async void InitVarsAndPosition()
        {
            var activityListy = Enum.GetNames(typeof(EventType)).ToList();
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
                        var helper = new EventHelper();
                        var newEvent = new EventShared
                        {
                            Date = DatePicker.Date,
                            Latitude = _selectedPosition.Latitude,
                            Longitude = _selectedPosition.Longitude,
                            EventType = (EventType) ActivityPicker.SelectedIndex,
                            UserId = 1,
                            Title = EventName.Text,
                            Description = EventDescription.Text,
                            UsersMax = 12
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
            return _selectedPosition != null;
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
            _selectedPosition = position;
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