using System;
using System.Collections.Generic;
using System.Text;
using OREventApp.Interfaces;
using Xamarin.Forms;

namespace OREventApp.Utilities
{
    class Connection
    {
        public static bool CheckInternetConnection()
        {
            return DependencyService.Get<IConnection>().CheckConnection();
        }

        public static void ShowNotificationNoInternetConnection()
        {

            DependencyService.Get<INotification>().Notify("No internet connection", 5000, "CLOSE", obj => { });

        }

        public static void ShowNotificationNoMarkerSelected()
        {

            DependencyService.Get<INotification>().Notify("No marker selected", 5000, "CLOSE", obj => { });

        }

        public static void ShowNotificationServerNotReachable()
        {

            DependencyService.Get<INotification>().Notify("Server not reachable", 5000, "CLOSE", obj => { });

        }

        public static void ShowNotificationNoActivitySelected()
        {

            DependencyService.Get<INotification>().Notify("No activity selected", 5000, "CLOSE", obj => { });

        }

    }
}
