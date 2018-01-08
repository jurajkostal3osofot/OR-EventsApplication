using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class Event
    {
        public long Id { get; set; }
        public DbGeography Location { get; set; }
    }
}
