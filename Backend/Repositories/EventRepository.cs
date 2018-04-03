using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Spatial;
using System.Linq;
using Backend.Interfaces.Repositories;
using Backend.Models;
using Shared.Models;

namespace Backend.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly DatabaseContext _databaseContext;
        public EventRepository(DatabaseContext context)
        {
            _databaseContext = context;
        }
        public void Add(Event newEvent)
        {
            _databaseContext.Events.Add(newEvent);
            _databaseContext.SaveChanges();
        }

        public void AddUserToEvent(UserEvent userEvent)
        {
            Event selectedEvent = Get(userEvent.EventId);

            if (selectedEvent != null)
            {
                selectedEvent.UserEvents.Add(userEvent); 
                _databaseContext.Events.Attach(selectedEvent);
                _databaseContext.SaveChanges();
            }
        }

        public void DeleteUserFromEvent(UserEvent userEvent)
        {
            Event selectedEvent = Get(userEvent.EventId);
            var selectedUserEvent = selectedEvent?.UserEvents.FirstOrDefault(x => x.UserId == userEvent.UserId);
            if (selectedUserEvent != null)
            {
                _databaseContext.UserEvents.Remove(selectedUserEvent);
                _databaseContext.SaveChanges();
            }
        }

        public void Edit(Event editEvent)
        {
            throw new NotImplementedException();
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public Event Get(long id)
        {
            return _databaseContext.Events.Include(x => x.User).Include(x => x.UserEvents.Select(y => y.User)).Include(x => x.User.Group).Single(x => x.Id == id);
        }

        public IEnumerable<Event> GetNearByTenEvents(PositionShared position)
        {
            DbGeography point = CreatePoint(position.Latitude.GetValueOrDefault(),position.Longitude.GetValueOrDefault());
            return _databaseContext.Events.Include(x => x.User).Include(x => x.UserEvents.Select(y => y.User)).Include(x => x.User.Group).Where(x => x.Location.Distance(point) < 2000000).Take(20);
        }

        public static DbGeography CreatePoint(double lat, double lon, int srid = 4326)
        {
            string wkt = String.Format("POINT({1} {0})", lat, lon);
            wkt = wkt.Replace(",", ".");
            return DbGeography.PointFromText(wkt, srid);
        }
    }
}