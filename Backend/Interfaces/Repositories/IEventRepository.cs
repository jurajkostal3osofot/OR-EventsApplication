using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using Backend.Models;
using Shared.Models;

namespace Backend.Interfaces.Repositories
{
    public interface IEventRepository
    {
        void Add(Event newEvent);
        void Edit(Event editEvent);
        void Delete(long id);
        void AddUserToEvent(UserEvent userEvent);
        void DeleteUserFromEvent(UserEvent userEvent);
        Event Get(long id);
        IEnumerable<Event> GetNearByTenEvents(PositionShared position);

    }
}