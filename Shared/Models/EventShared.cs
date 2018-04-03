using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using Shared.Enums;

namespace Shared.Models
{
    public class EventShared
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public EventType EventType { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public long UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int UsersMax { get; set; }
        public int NumberOfLoggedInUsers { get; set; }
        public User User { get; set; }
        public List<User> LoggedInUsers { get; set; }
    }
}
