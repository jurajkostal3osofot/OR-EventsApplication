using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Plugin.Geolocator;

namespace OREventApp.Helpers
{
    class LocationHelper
    {
        public static async Task<Plugin.Geolocator.Abstractions.Position> GetCurrentLocation()
        {
            //Geolocator position
            Plugin.Geolocator.Abstractions.Position position = null;
            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 25;
                position = await locator.GetLastKnownLocationAsync();
                if (position != null)
                {
                    return position;
                }

                if (!locator.IsGeolocationAvailable || !locator.IsGeolocationEnabled)
                {
                    //not available or enabled
                    return null;
                }

                position = await locator.GetPositionAsync(TimeSpan.FromSeconds(20), null, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                //Display error as we have timed out or can't get location.
            }

            return position;
        }
    }
}
