using System;
using System.Collections.Generic;
using System.Text;
using OREventApp.Interfaces;
using Xamarin.Forms;

namespace OREventApp.Utilities
{
    class Notifications
    {
        public static void ShowNumberEvents(int numberEvents)
        {
            DependencyService.Get<INotification>().ShowNumberEvents(string.Format("{0} events near your location",numberEvents), 5000, obj => { });
        }
    }
}
