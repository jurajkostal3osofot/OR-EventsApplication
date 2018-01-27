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

            _databaseContext.Events.Add(newEvent);
            _databaseContext.SaveChanges();
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
            DbGeography point = CreatePoint(48.1486, 17.1077);
            //return _databaseContext.Events.OrderBy(x => x.Location.Distance(point)).Take(20);20000000
            return _databaseContext.Events.Where(x => x.Location.Distance(point) < 2000000).Take(20);
//            return _databaseContext.Events.Where(x => true);
        }

        public static DbGeography CreatePoint(double lat, double lon, int srid = 4326)
        {
            string wkt = String.Format("POINT({1} {0})", lat, lon);
            wkt = wkt.Replace(",", ".");
            return DbGeography.PointFromText(wkt, srid);
        }
    }
}