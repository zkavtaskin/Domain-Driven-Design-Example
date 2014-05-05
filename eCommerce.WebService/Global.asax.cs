using Castle.Windsor;
using eCommerce.WebService;
using eCommerce.WebService.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace eCommerce.WebService
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private IWindsorContainer container;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            MappingConfig.RegisterMapping();

            container = new WindsorContainer();
            BootstrapConfig.Register(this.container);
        }

        protected void Application_Stop()
        {
            if(this.container != null)
            {
                this.container.Dispose();
            }
        }

    }
}