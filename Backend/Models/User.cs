using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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
    }
}