using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Models
{
    public class UserEvent
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long EventId { get; set; }
        public User User { get; set; }
        public Event Event { get; set; }
    }
}