using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using NNI.PayerPortal.Domain.Entities;
using NNI.PayerPortal.WebUI.Infrastructure;

namespace NNI.PayerPortal.WebUI
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(null,
            //    "", // Only matches the empty URL (i.e. /)
            //    new
            //    {
            //        controller = "Home", // "Resource",
            //        action = "Index", // "List", 
            //        category = (string)null,
            //        page = 1
            //    }
            //);

            //routes.MapRoute(null,
            //    "Page{page}", // Matches /Page2, /Page123, but not /PageXYZ
            //    new { controller = "Resource", action = "List", category = (string)null },
            //    new { page = @"\d+" } // Constraints: page must be numerical
            //);

            //routes.MapRoute(null,
            //    "{category}", // Matches /SomeResourceCategory or /AnythingWithNoSlash
            //    new { controller = "Resource", action = "List", page = 1 }
            //);

            //routes.MapRoute(null,
            //    "{category}/Page{page}", // Matches /SomeResourceCategory/Page567
            //    new { controller = "Resource", action = "List" }, // Defaults
            //    new { page = @"\d+" } // Constraints: page must be numerical
            //);
            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
            
            routes.MapRoute(null, "{controller}/{action}");



        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            // Register the Ninject Controller Factory
            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());
        }
    }
}