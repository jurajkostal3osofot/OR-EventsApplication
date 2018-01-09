using OREventApp.Droid.DepedencyServices;
using OREventApp.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(Connection))]
namespace OREventApp.Droid.DepedencyServices
{
    class Connection : IConnection
    {
        public bool CheckConnection()
        {
            return true;
        }
    }
}