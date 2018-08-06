using System.Reflection;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using HouseBooking.Backend.Repository;
using HouseBooking.Backend.Repository.SQLite;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;

namespace HouseBooking.UI.MVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            var sqliteConnectionString = $"URI=file:{Server.MapPath("~")}\\bin\\Database\\HouseBooking.Data.sqlite";
            container.Register<IHousingRepository>(() => new SqliteHousingRepository(sqliteConnectionString), Lifestyle.Scoped);
            container.Register<IReservationRepository>(()=> new SqliteReservationRepository(sqliteConnectionString), Lifestyle.Scoped);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            //container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}
