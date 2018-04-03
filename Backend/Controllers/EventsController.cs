using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web.Http;
using Backend.Interfaces.Repositories;
using Backend.Models;
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
        [Route("api/events/{latitude}/{longitude}")]
        public IEnumerable<EventShared> GetEvents(string latitude,string longitude)
        {
            double? latDecimal = double.TryParse(latitude.Replace(",","."), out var temp) ? temp : default(double?);
            double? lonDecimal = double.TryParse(longitude.Replace(",", "."), out temp) ? temp : default(double?);
            
            PositionShared position =  new PositionShared
            {
                Latitude = latDecimal,
                Longitude = lonDecimal
            };
            var events = _eventRepository.GetNearByTenEvents(position).Select(eventLoaded => new EventShared
            {
                Date = eventLoaded.Date,
                EventType = eventLoaded.EventType,
                Id = eventLoaded.Id,
                Latitude = eventLoaded.Location.Latitude,
                Longitude = eventLoaded.Location.Longitude,
                Title = eventLoaded.Title,
                Description = eventLoaded.Description,
                UsersMax = eventLoaded.UsersMax,
                NumberOfLoggedInUsers = eventLoaded.UserEvents.Count,
                LoggedInUsers = eventLoaded.UserEvents.Select(x => new Shared.Models.User
                {
                    Id = x.UserId,
                    Password = x.User.Password,
                    Email = x.User.Email,
                    GroupId = x.User.GroupId,
                    Group = new Shared.Models.Group
                    {
                        Id = x.User.Group.Id,
                        Name = x.User.Group.Name,
                    }
                }).ToList()

            });

            return events;
        }

        [Route("api/event/get/{id}")]
        public EventShared GetEvent(long id)
        {
            var eventLoaded = _eventRepository.Get(id);
            var eventShared = new EventShared
            {
                Id = eventLoaded.Id,
                UserId = eventLoaded.UserId,
                Date = eventLoaded.Date,
                EventType = eventLoaded.EventType,
                Latitude = eventLoaded.Location.Latitude,
                Longitude = eventLoaded.Location.Longitude,
                Title = eventLoaded.Title,
                Description = eventLoaded.Description,
                UsersMax = eventLoaded.UsersMax,
                NumberOfLoggedInUsers = eventLoaded.UserEvents.Count,
                LoggedInUsers = eventLoaded.UserEvents.Select(x => new Shared.Models.User
                {
                    Id = x.UserId,
                    Password = x.User.Password,
                    Email = x.User.Email,
                    GroupId = x.User.GroupId,
                    Group = new Shared.Models.Group
                    {
                        Id = x.User.Group.Id,
                        Name = x.User.Group.Name,
                    }
                }).ToList()
            };
            return eventShared;
        }

        // POST api/values
        public IHttpActionResult Post([FromBody] EventShared result)
        {
            _eventRepository.Add(new Event
            {
                Title = result.Title,
                Description = result.Description,
                UserId = result.UserId,
                UsersMax = result.UsersMax,
                EventType = result.EventType,
                Location = CreatePoint(result.Latitude.GetValueOrDefault(), result.Longitude.GetValueOrDefault()),
                Date = result.Date
            });
            return Ok();
        }

        [Route("api/event/join")]
        [HttpPost]
        public IHttpActionResult JoinToEvent([FromBody] EventShared result)
        {
            var userEvent = new UserEvent
            {
                UserId = result.UserId,
                EventId = result.Id
            };
            _eventRepository.AddUserToEvent(userEvent);

            return Ok();
        }

        [Route("api/event/leave")]
        [HttpPost]
        public IHttpActionResult LeaveFromEvent([FromBody] EventShared result)
        {
            var userEvent = new UserEvent
            {
                UserId = result.UserId,
                EventId = result.Id
            };
            _eventRepository.DeleteUserFromEvent(userEvent);

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