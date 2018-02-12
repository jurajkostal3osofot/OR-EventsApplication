using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Models
{
    public class GroupClaim
    {
        public long Id { get; set; }
        public long? GroupId { get; set; }
        public long ClaimId { get; set; }
        public Claim Claim { get; set; }
        public Group Group { get; set; }
    }
}