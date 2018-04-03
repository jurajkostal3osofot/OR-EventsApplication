using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using Shared.Enums;

namespace Backend.Models
{
    public class Event
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public EventType EventType { get; set; }
        public DbGeography Location { get; set; }
        [ForeignKey(nameof(User))]
        public long UserId { get; set; }
        public User User { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int UsersMax { get; set; }
        public ICollection<UserEvent> UserEvents { get; set; }
    }
}