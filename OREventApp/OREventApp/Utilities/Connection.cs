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
    }
}
