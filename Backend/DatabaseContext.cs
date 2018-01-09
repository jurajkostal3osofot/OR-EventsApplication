using System.Data.Entity;
using Backend.Models;

namespace Backend
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Event> Events { get; set; }
    }
}