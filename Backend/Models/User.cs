﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public long GroupId { get; set; }
        public Group Group { get; set; }
        [Required]
        public String Password { get; set; }
        public ICollection<Event> Events { get; set; }
        public ICollection<UserEvent> UserEvents { get; set; }
    }
}