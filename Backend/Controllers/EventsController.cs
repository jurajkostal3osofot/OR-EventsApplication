using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web;
using System.Web.Http;
using Backend.Enums;
using Backend.Interfaces.Repositories;
using Backend.Models;
using Newtonsoft.Json;
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
        public IHttpActionResult Post([FromBody]EventShared result)
        {
            _eventRepository.Add(new Event
            {
                EventType = (EventType)result.EventType,
                Location = CreatePoint(result.Latitude.GetValueOrDefault(),result.Longitude.GetValueOrDefault()),
                Date = result.Date
            });
            return Ok();
        }

        public static DbGeography CreatePoint(double lat, double lon, int srid = 4326)
        {
            string wkt = String.Format("POINT({1} {0})", lat, lon);
            wkt = wkt.Replace(",", ".");
            return DbGeography.PointFromText(wkt, srid);
        }
    }
}