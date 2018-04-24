using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Cuidadores.Web.Infrastructure;

namespace Cuidadores.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //ModelBinders.Binders.Add(typeof(DateTime), new DateTimeModelBinder());
            //ModelBinders.Binders.Add(typeof(DateTime?), new DateTimeModelBinder());
        }
    }
}
