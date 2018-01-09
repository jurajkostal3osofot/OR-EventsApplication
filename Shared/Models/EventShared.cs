using System;
using System.Data.Entity.Spatial;
using Shared.Enums;

namespace Shared.Models
{
    public class EventShared
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public EventTypeShared EventType { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}
