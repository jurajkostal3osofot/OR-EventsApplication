using System.Data.Entity.Spatial;
using Backend.Enums;
using Backend.Models;

namespace Backend.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Backend.DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Backend.DatabaseContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            Random rng = new Random();


            for (int i = 0; i < 30000; i++)
            {
                int lat = rng.Next(-900000000, 900000000);
                int lon = rng.Next(-1800000000, 1800000000);
                double lt = lat / 10000000d;
                double ln = lon / 10000000d;
                Event newEvent = new Event
                {
                    Date = DateTime.Now,
                    EventType = EventType.Florball,
                    Location = CreatePoint(lt, ln)
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
