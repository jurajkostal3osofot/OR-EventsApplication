using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OREventApp.Domain;
using OREventApp.Helpers;
using OREventApp.Renderers;
using OREventApp.Utilities;
using Plugin.Geolocator;
using Shared.Models;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace OREventApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IndexPage : ContentPage
    {
        private IndexMap _map;
        internal static List<EventShared> _eventShared;
        private List<string> _markerIds;
        private readonly List<double> _latitudes;
        private readonly List<double> _longitudes;

        public IndexPage()
        {
            _eventShared = new List<EventShared>();
            _markerIds = new List<string>();
            _latitudes = new List<double>();
            _longitudes = new List<double>();
            InitializeComponent();
            InnitMap();
        }

        public async void InnitMap()
        {
            var position = await LocationHelper.GetCurrentLocation();
            _map = new IndexMap(MapSpan.FromCenterAndRadius(new Position(position.Latitude,position.Longitude), Distance.FromKilometers(2)))
            {
                IsShowingUser = true,
                HeightRequest = 320,
                WidthRequest = 200,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            _map.MarkerClicked += MarkerClickedEvent;
            
            var stack = new StackLayout {Spacing = 0};
            stack.Children.Add(_map);
            Content = stack;
            LoadPins();
        }

        private async void MarkerClickedEvent(object sender, MarkerClickEventArgs e)
        {
            int markerIndex = _markerIds.FindIndex(x => x.StartsWith(e.Id));
            if (markerIndex > 0 && markerIndex < _eventShared.Count)
            {
                EventShared clickedEvent = _eventShared[markerIndex];
                //clickedEvent.Id = 10;
                EventShared newEvent = await new EventHelper().GetEventAsync(clickedEvent.Id);
                await Navigation.PushModalAsync(new DetailEventPage(newEvent));
            }
        }

        private async void LoadPins()
        {
            if (Connection.CheckInternetConnection())
            {
                EventHelper eventHelper = new EventHelper();
                var position = await LocationHelper.GetCurrentLocation();
                IEnumerable<EventShared> events = await eventHelper.GetEventsAsync();
                //_map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude), Distance.FromKilometers(2)));

                if (events != null)
                {
                    _eventShared = events.ToList();
                    foreach (var loadedEvent in _eventShared)
                    {
                        var pin = new Pin
                        {
                            Id = (int)loadedEvent.EventType,
                            Type = PinType.Place,
                            Position = new Position(loadedEvent.Latitude.GetValueOrDefault(), loadedEvent.Longitude.GetValueOrDefault()),
                            Label = ""
                        };
                        _map.Pins.Add(pin);
                        _markerIds.Add(_map.Pins[_map.Pins.Count - 1].Id.ToString());
                        _latitudes.Add(pin.Position.Latitude);
                        _longitudes.Add(pin.Position.Longitude);
                    }

                    if (position != null)
                    {
                        //CenterMarkersOnMap(new Position(position.Latitude, position.Longitude));
                        Notifications.ShowNumberEvents(_map.Pins.Count);
                    }

                    else
                        await DisplayAlert("No GPS", "We could not obtain your location.", "RIP");

                }
                else
                    Connection.ShowNotificationServerNotReachable();
            }
            else
            {
                Connection.ShowNotificationNoInternetConnection();
            }
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