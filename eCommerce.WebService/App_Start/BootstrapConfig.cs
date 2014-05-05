using Castle.Windsor;
using eCommerce.WebService.App_Start.Installers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;

namespace eCommerce.WebService.App_Start
{
    public class BootstrapConfig
    {
        public static void Register(IWindsorContainer container)
        {
            GlobalConfiguration.Configuration.Services.Replace(
                typeof(IHttpControllerActivator),
                new WindsorCompositionRoot(container));

            container.Install(
                new InfrastructureLayerInstall(),
                new ApplicationLayerInstall(),
                new DomainModelLayerInstall(),
                new DistributedInterfaceLayerInstall()
            );
        }
    }
}