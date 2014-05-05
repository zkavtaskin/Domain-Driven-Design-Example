using Castle.MicroKernel.Registration;
using eCommerce.DomainModelLayer;
using eCommerce.DomainModelLayer.Countries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eCommerce.WebService.App_Start.Installers
{
    public class DomainModelLayerInstall : Castle.MicroKernel.Registration.IWindsorInstaller
    {
        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(Classes.FromAssemblyNamed("eCommerce")
                .BasedOn(typeof(eCommerce.Helpers.Domain.Handles<>))
                .WithService.FromInterface(typeof(eCommerce.Helpers.Domain.Handles<>))
                .Configure(c => c.LifestyleTransient()));

            container.Register(Classes.FromAssemblyNamed("eCommerce")
                .BasedOn<eCommerce.Helpers.Domain.IDomainService>()
                .WithService.DefaultInterfaces()
                .Configure(c => c.LifestyleTransient()));

            container.Register(Component.For<Settings>()
                .Instance(new Settings(Country.Create(new Guid("229074BD-2356-4B5A-8619-CDEBBA71CC21"), "United Kingdom"))
                    )
               );
        }
    }
}