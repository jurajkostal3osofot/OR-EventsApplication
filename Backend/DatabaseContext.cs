using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;

namespace Backend
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Event> Events { get; set; }

        public DatabaseContext()
        {

        }
    }
}
