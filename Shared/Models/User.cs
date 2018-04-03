using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public long GroupId { get; set; }
        public Group Group { get; set; }
        public String Password { get; set; }
        public IEnumerable<EventShared> Events { get; set; }
    }
}
