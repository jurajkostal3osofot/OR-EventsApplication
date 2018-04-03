using System.Data.Entity.Spatial;
using Backend.Models;
using Shared.Enums;
using System;
using System.Data.Entity.Migrations;

namespace Backend.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DatabaseContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            Group group = new Group
            {
                Name = "Default"
            };
            context.Groups.Add(group);
            context.SaveChanges();

            User user = new User
            {
                Email = "test@test.com",
                Password = "test",
                GroupId = 1
            };
            context.Users.Add(user);
            context.SaveChanges();

            Random rng = new Random();
            for (int i = 0; i < 300; i++)
            {
                int lat = rng.Next(-900000000, 900000000);
                int lon = rng.Next(-1800000000, 1800000000);
                double lt = lat / 10000000d;
                double ln = lon / 10000000d;
                Event newEvent = new Event
                {
                    Date = DateTime.Now,
                    EventType = EventType.Florball,
                    Location = CreatePoint(lt, ln),
                    UserId = 1,
                    Title = $"Title {rng.Next(1,100000)}",
                    Description = $"Description {rng.Next(1, 100000)}",
                    UsersMax = rng.Next(2,50)
                };
                context.Events.Add(newEvent);
            }
            context.SaveChanges();
        }

        public static DbGeography CreatePoint(double lat, double lon, int srid = 4326)
        {
            string wkt = String.Format("POINT({1} {0})", lat, lon);
            wkt = wkt.Replace(",", ".");
            return DbGeography.PointFromText(wkt, srid);
        }
    }
}
