using System.Web.Http;
using Backend.Interfaces.Repositories;
using Unity;
using Unity.WebApi;
using Backend.Repositories;


namespace Backend
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            container.RegisterType<IEventRepository, EventRepository>();
            container.RegisterType<DatabaseContext>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}