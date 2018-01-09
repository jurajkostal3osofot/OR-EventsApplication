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
        }

        public async Task<bool> SaveEventAsync(EventShared newEvent)
        {
            var uri = new Uri(string.Format(Constants.EventsUrl, string.Empty));

            var json = JsonConvert.SerializeObject(newEvent);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;
            
            response = await _client.PostAsync(uri, content);
            

            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<EventShared>> GetEventsAsync()
        {
            var uri = new Uri(string.Format(Constants.EventsUrl, string.Empty));

            var content = await _client.GetStringAsync(uri);

            IEnumerable<EventShared> events = JsonConvert.DeserializeObject<List<EventShared>>(content);
            return events;
        }

    }
}
