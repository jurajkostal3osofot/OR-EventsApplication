using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using Backend.Interfaces.Repositories;
using Backend.Models;

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

            throw new NotImplementedException();
        }

        public void Edit(Event editEvent)
        {
            throw new NotImplementedException();
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public Event Get(int id)
        {
            return _databaseContext.Events.Single(x => x.Id == id);
        }

        public IEnumerable<Event> GetNearByTenEvents()
        {
            DbGeography point = CreatePoint(49.0511, 20.2954);
            return _databaseContext.Events.OrderBy(x => x.Location.Distance(point)).Take(20);
        }

        public static DbGeography CreatePoint(double lat, double lon, int srid = 4326)
        {
            string wkt = String.Format("POINT({1} {0})", lat, lon);
            wkt = wkt.Replace(",", ".");
            return DbGeography.PointFromText(wkt, srid);
        }
    }
}