using System.Data.Entity;
using Backend.Models;

namespace Backend
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<GroupClaim> GroupClaims { get; set; }
        public DbSet<UserEvent> UserEvents { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // configures one-to-many relationship
            modelBuilder.Entity<UserEvent>()
                .HasRequired(d => d.User)
                .WithMany(w => w.UserEvents)
                .HasForeignKey(d => d.UserId)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<UserEvent>()
                .HasRequired(d => d.Event)
                .WithMany(w => w.UserEvents)
                .HasForeignKey(d => d.EventId)
                .WillCascadeOnDelete(false);
        }
    }
}