using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Models
{
    public class Claim
    {
        public long Id { get; set; }
        public short Value { get; set; }
        public ICollection<GroupClaim> GroupClaims { get; set; }
    }
}