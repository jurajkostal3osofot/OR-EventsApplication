using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Backend.Interfaces.Repositories;
using Backend.Models;
using Shared.Enums;
using Shared.Models;

namespace Backend.Controllers
{
    public class EventsController : ApiController
    {
        private readonly IEventRepository _eventRepository;
        public EventsController(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }
        // GET api/values
        public IEnumerable<EventShared> Get()
        {
            var events = _eventRepository.GetNearByTenEvents().Select(f => new EventShared
            {
                Date = f.Date,
                EventType = (EventTypeShared)f.EventType,
                Id = f.Id,
                Latitude = f.Location.Latitude,
                Longitude = f.Location.Longitude
            });

            return events;
        }

        public EventShared Get(int id)
        {
            var eventLoaded = _eventRepository.Get(id);
            return new EventShared
            {   
                Id = eventLoaded.Id,
                Date = eventLoaded.Date,
                EventType = (EventTypeShared)eventLoaded.EventType,
                Latitude = eventLoaded.Location.Latitude,
                Longitude = eventLoaded.Location.Longitude
            };
        }

        // POST api/values
        public IHttpActionResult Post([FromBody]Event result)
        {
            //var ret = JsonConvert.DeserializeObject <Event> (result);
            return Ok();
        }
    }
}