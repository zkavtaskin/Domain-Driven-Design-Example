using Castle.MicroKernel.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eCommerce.WebService.App_Start.Installers
{
    public class ApplicationLayerInstall : Castle.MicroKernel.Registration.IWindsorInstaller
    {
        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(Classes.FromAssembly(typeof(eCommerce.ApplicationLayer.Customers.ICustomerService).Assembly)
                .Where(x => !x.IsInterface && !x.IsAbstract && !x.Name.EndsWith("Dto") && x.Namespace.Contains("ApplicationLayer"))
                .WithService.DefaultInterfaces()
                .Configure(c => c.LifestyleTransient()));
        }
    }
}