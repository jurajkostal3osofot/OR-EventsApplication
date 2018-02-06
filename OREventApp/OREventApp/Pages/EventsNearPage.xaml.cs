using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OREventApp.Helpers;
using OREventApp.Models;
using OREventApp.Utilities;
using Shared.Models;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace OREventApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventsNearPage : ContentPage
    {
        private IEnumerable<EventShared> events;
        private ObservableCollection<CellModel> listOfCollection;
        public EventsNearPage()
        {
            InitializeComponent();
            if (Connection.CheckInternetConnection())
                LoadEventsToListView();
            else
            {
                Connection.ShowNotificationNoInternetConnection();
            }
        }

        public static string getMapUrl(double lat, double lon, int width, int height, int eventType)
        {
            string coordPair = lat + "," + lon;
            //TODO: docasne linky na dropbox kedze server nieje pripojeny k internetu. 
            //string iconUrl = Constants.ImagesUrl;
            string iconUrl;
            switch (eventType)
            {
                case 1:
                    iconUrl = "https://www.dropbox.com/s/9boa226ql57vyyn/ic_bike.png?raw=1";
                    break;
                case 2:
                    iconUrl = "https://www.dropbox.com/s/lrts94o6hp4xrdv/ic_sky.png?raw=1";
                    break;
                case 3:
                    iconUrl = "https://www.dropbox.com/s/xb4m0ut5707ziyp/ic_soccer.png?raw=1";
                    break;
                default:
                    iconUrl = "https://www.dropbox.com/s/xb4m0ut5707ziyp/ic_soccer.png?raw=1";
                    break;

            }
            Debug.WriteLine("http://maps.googleapis.com/maps/api/staticmap?" + "&zoom=17" + "&size=" + width + "x" + height +
                            "&maptype=roadmap&sensor=true" + "&center=" + coordPair + "&markers=icon:" + iconUrl + "|" + coordPair);
            return "http://maps.googleapis.com/maps/api/staticmap?" + "&zoom=17" + "&size=" + width + "x" + height +
                   "&maptype=roadmap&sensor=true" + "&center=" + coordPair + "&markers=icon:" + iconUrl + "|" + coordPair;
        }

        private async void LoadEventsToListView()
        {
            EventHelper helper = new EventHelper();
            events = await helper.GetEventsAsync();
            if (events == null)
            {
                Connection.ShowNotificationServerNotReachable();
            }
            else
            {
                listOfCollection = new ObservableCollection<CellModel>();
                //ActivityList.IsPullToRefreshEnabled = true;
                ActivityList.ItemSelected += (sender, e) => { ((ListView) sender).SelectedItem = null; };
                foreach (var loadedEvent in events)
                {
                    
                    listOfCollection.Add(new CellModel()
                    {
                        Heading = loadedEvent.EventType + " at " + loadedEvent.Date.ToString("HH':'mm"),
                        MiniMap = getMapUrl((double) loadedEvent.Latitude, (double) loadedEvent.Longitude, 200, 200, (int)loadedEvent.EventType),
                        NumberOfAttendates = "0 attendates"
                    });
                }
                ActivityList.ItemsSource = listOfCollection;
                
            }
        }

        private void Join_OnClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Details_OnClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}