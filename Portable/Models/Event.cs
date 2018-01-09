using System;

using System.Data.Entity.Spatial;
using Portable.Enums;

namespace Portable.Models
{
    public class Event
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public EventType EventType { get; set; }
        public DbGeography Location { get; set; }
              
    }
}
