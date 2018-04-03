using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Shared.Models;
using Xamarin.Forms.Maps;

namespace OREventApp.Helpers
{
    
    class EventHelper
    {
        private readonly HttpClient _client;

        public EventHelper()
        {
            _client = new HttpClient();
            _client.MaxResponseContentBufferSize = 256000;
            _client.Timeout = TimeSpan.FromSeconds(10);
        }

        public async Task<bool> SaveEventAsync(EventShared newEvent)
        {
            var uri = new Uri(string.Format(Constants.EventsUrl, string.Empty));

            var json = JsonConvert.SerializeObject(newEvent);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response;
            
            try
            {
                response = await _client.PostAsync(uri, content);
            }
            catch (HttpRequestException)
            {
                return false;
            }
            catch (TaskCanceledException)
            {
                return false;
            }


            return response.IsSuccessStatusCode;
        }

        public async Task<bool> JoinToEventAsync(long eventId, long userId)
        {
            var uri = new Uri(string.Format(Constants.JoinToEventUrl, string.Empty));

            var json = JsonConvert.SerializeObject(new EventShared{Id = eventId, UserId = userId});
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response;

            try
            {
                response = await _client.PostAsync(uri, content);
            }
            catch (HttpRequestException)
            {
                return false;
            }
            catch (TaskCanceledException)
            {
                return false;
            }


            return response.IsSuccessStatusCode;
        }

        public async Task<bool> LeaveFromEventAsync(long eventId, long userId)
        {
            var uri = new Uri(string.Format(Constants.LeaveFromEventUrl, string.Empty));

            var json = JsonConvert.SerializeObject(new EventShared { Id = eventId, UserId = userId });
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response;

            try
            {
                response = await _client.PostAsync(uri, content);
            }
            catch (HttpRequestException)
            {
                return false;
            }
            catch (TaskCanceledException)
            {
                return false;
            }


            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<EventShared>> GetEventsAsync()
        {
            //var uri = new Uri(string.Format(Constants.EventsUrl, string.Empty));
            var position =  await LocationHelper.GetCurrentLocation();
            var uri = new Uri(string.Format(Constants.EventsUrl,position.Latitude,position.Longitude));
            var content = "";
            IEnumerable<EventShared> events = new List<EventShared>();

            try
            {
                content = await _client.GetStringAsync(uri);
                events = JsonConvert.DeserializeObject<List<EventShared>>(content);
            }
            catch (HttpRequestException)
            {                
                Console.WriteLine("HttpRequestException");
            }
            catch (TaskCanceledException)
            {
                Console.WriteLine("TaskCanceledException");
            }            
            
            return events;
        }

        public async Task<EventShared> GetEventAsync(long id)
        {
            var uri = new Uri(string.Format(Constants.EventUrl,id));
            EventShared eventShared = new EventShared();

            try
            {
                var content = await _client.GetStringAsync(uri);
                eventShared = JsonConvert.DeserializeObject<EventShared>(content);
            }
            catch (HttpRequestException)
            {
                Console.WriteLine("HttpRequestException");
            }
            catch (TaskCanceledException)
            {
                Console.WriteLine("TaskCanceledException");
            }

            return eventShared;
        }

    }
}
