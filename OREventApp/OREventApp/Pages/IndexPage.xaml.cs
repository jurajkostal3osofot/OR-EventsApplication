using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using OREventApp.Domain;
using OREventApp.Helpers;
using OREventApp.Renderers;
using OREventApp.Utilities;
using Plugin.Geolocator;
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
        private readonly List<double> _latitudes;
        private readonly List<double> _longitudes;

        public IndexPage()
        {
            InitializeComponent();
            InnitMap();
            if (Connection.CheckInternetConnection())
            {
                LoadPins();
            }
            else
            {
                Connection.ShowNotificationNoInternetConnection();
            }

            _latitudes = new List<double>();
            _longitudes = new List<double>();
        }

        public void InnitMap()
        {
            _map = new IndexMap(MapSpan.FromCenterAndRadius(new Position(37, -122), Distance.FromMiles(0.3)))
            {
                IsShowingUser = true,
                HeightRequest = 320,
                WidthRequest = 200,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            
            var stack = new StackLayout {Spacing = 0};
            stack.Children.Add(_map);
            Content = stack;
        }

        private async void LoadPins()
        {
            EventHelper helper = new EventHelper();
            IEnumerable<EventShared> events = await helper.GetEventsAsync();
            Plugin.Geolocator.Abstractions.Position position = await LocationHelper.GetCurrentLocation();
            _map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude),
                Distance.FromKilometers(2)));

            if (events != null)
            {
                foreach (var loadedEvent in events)
                {
                    var pin = new Pin
                    {
                        Id = (int) loadedEvent.EventType,
                        Type = PinType.Place,
                        Position = new Position(loadedEvent.Latitude.GetValueOrDefault(),
                            loadedEvent.Longitude.GetValueOrDefault()),
                        Label = ""
                    };
                    _map.Pins.Add(pin);
                    _latitudes.Add(pin.Position.Latitude);
                    _longitudes.Add(pin.Position.Longitude);
                }

                if (position != null)
                {
                    CenterMarkersOnMap(new Position(position.Latitude, position.Longitude));
                    Notifications.ShowNumberEvents(_map.Pins.Count);
                }
                    
                else
                    await DisplayAlert("No GPS", "We could not obtain your location.", "RIP");
                
            }
            else
                Connection.ShowNotificationServerNotReachable();
        }

        private void CenterMarkersOnMap(Position pos)
        {
            _latitudes.Add(pos.Latitude);
            _longitudes.Add(pos.Longitude);
            double lowestLat = _latitudes.Min();
            double highestLat = _latitudes.Max();
            double lowestLong = _longitudes.Min();
            double highestLong = _longitudes.Max();
            double finalLat = (lowestLat + highestLat) / 2;
            double finalLong = (lowestLong + highestLong) / 2;
            double distance = DistanceCalculation.GeoCodeCalc.CalcDistance(lowestLat, lowestLong, highestLat,
                highestLong, DistanceCalculation.GeoCodeCalcMeasurement.Kilometers);
            _map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(finalLat, finalLong),
                Distance.FromKilometers(distance)));
        }

        
    }
}