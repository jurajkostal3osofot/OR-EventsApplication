using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using Backend.Models;

namespace Backend.Interfaces.Repositories
{
    public interface IEventRepository
    {
        void Add(Event newEvent);
        void Edit(Event editEvent);
        void Delete(long id);
        Event Get(int id);
        IEnumerable<Event> GetNearByTenEvents();

    }
}