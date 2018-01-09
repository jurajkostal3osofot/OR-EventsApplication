using Android.Content;
using Android.Net;
using OREventApp.Droid.DepedencyServices;
using OREventApp.Interfaces;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(Connection))]
namespace OREventApp.Droid.DepedencyServices
{
    class Connection : IConnection
    {
        public bool CheckConnection()
        {
            var connectivityManager = (ConnectivityManager)Forms.Context.ApplicationContext.GetSystemService(Context.ConnectivityService);
            NetworkInfo networkInfo = connectivityManager.ActiveNetworkInfo;
            bool isConnected = networkInfo != null && networkInfo.IsConnectedOrConnecting;
            return isConnected;
        }
    }
}