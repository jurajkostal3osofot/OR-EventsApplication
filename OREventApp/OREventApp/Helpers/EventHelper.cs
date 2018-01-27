using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Shared.Models;

namespace OREventApp.Helpers
{
    
    class EventHelper
    {

        private readonly HttpClient _client;

        public EventHelper()
        {
            _client = new HttpClient();
            _client.MaxResponseContentBufferSize = 256000;
            _client.Timeout = TimeSpan.FromSeconds(5);
        }

        public async Task<bool> SaveEventAsync(EventShared newEvent)
        {
            var uri = new Uri(string.Format(Constants.EventsUrl, string.Empty));

            var json = JsonConvert.SerializeObject(newEvent);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;
            
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
            var uri = new Uri(Constants.EventsUrl);
            var content = "";
            IEnumerable<EventShared> events;

            try
            {
                content = await _client.GetStringAsync(uri);
                events = JsonConvert.DeserializeObject<List<EventShared>>(content);
            }
            catch (HttpRequestException)
            {
                
                Console.WriteLine("HttpRequestException");
                events = null;
            }
            catch (TaskCanceledException)
            {
                Console.WriteLine("TaskCanceledException");
                events = null;
            }
            
            
            return events;
        }

    }
}
