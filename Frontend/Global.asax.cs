using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Frontend.App_Start;
using MongoDataAccess;
using TaskScheduler;
using TaskScheduler.EventBus;
using TaskScheduler.EventHandlers;
using TaskScheduler.Events;
namespace Frontend
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ServiceBusConfig.Initialize();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            TaskSchedulerConfig.Start();
        }
    }
}
