﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Camelonta.Backend.Repository;
using Camelonta.Backend.Repository.SQLite;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;

namespace CamelontaBooking
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Create the container as usual.
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            // Register your types, for instance:

            var sqliteConnectionString = $"URI=file:{Server.MapPath("~")}\\bin\\Database\\Camelonta.Data.sqlite";
            container.Register<IHousingRepository>(() => new SqliteHousingRepository(sqliteConnectionString), Lifestyle.Scoped);
            container.Register<IReservationRepository>(()=> new SqliteReservationRepository(sqliteConnectionString), Lifestyle.Scoped);

            // This is an extension method from the integration package.
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            //container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));

        }
    }
}
