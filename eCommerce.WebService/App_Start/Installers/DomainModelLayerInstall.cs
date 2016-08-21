using Castle.MicroKernel.Registration;
using eCommerce.DomainModelLayer;
using eCommerce.DomainModelLayer.Carts;
using eCommerce.DomainModelLayer.Countries;
using eCommerce.DomainModelLayer.Customers;
using eCommerce.DomainModelLayer.Products;
using eCommerce.DomainModelLayer.Tax;
using eCommerce.Helpers.Domain;
using System;

namespace eCommerce.WebService.App_Start.Installers
{
    public class DomainModelLayerInstall : Castle.MicroKernel.Registration.IWindsorInstaller
    {
        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(Classes.FromAssemblyNamed("eCommerce")
                .BasedOn(typeof(Handles<>))
                .WithService.FromInterface(typeof(Handles<>))
                .Configure(c => c.LifestyleTransient()));

            container.Register(Classes.FromAssemblyNamed("eCommerce")
                .BasedOn<IDomainService>()
                .WithService.DefaultInterfaces()
                .Configure(c => c.LifestyleTransient()));

            container.Register(Component.For<Settings>()
                .Instance(new Settings(Country.Create(new Guid("229074BD-2356-4B5A-8619-CDEBBA71CC21"), "United Kingdom"))
                    )
               );

            container.Register(Component.For<Handles<CartCreated>>().ImplementedBy<DomainEventHandle<CartCreated>>().LifestyleSingleton());
            container.Register(Component.For<Handles<ProductAddedCart>>().ImplementedBy<DomainEventHandle<ProductAddedCart>>().LifestyleSingleton());
            container.Register(Component.For<Handles<ProductRemovedCart>>().ImplementedBy<DomainEventHandle<ProductRemovedCart>>().LifestyleSingleton());
            container.Register(Component.For<Handles<CountryCreated>>().ImplementedBy<DomainEventHandle<CountryCreated>>().LifestyleSingleton());
            container.Register(Component.For<Handles<CreditCardAdded>>().ImplementedBy<DomainEventHandle<CreditCardAdded>>().LifestyleSingleton());
            container.Register(Component.For<Handles<CustomerChangedEmail>>().ImplementedBy<DomainEventHandle<CustomerChangedEmail>>().LifestyleSingleton());
            container.Register(Component.For<Handles<CustomerCheckedOut>>().ImplementedBy<DomainEventHandle<CustomerCheckedOut>>().LifestyleSingleton());
            container.Register(Component.For<Handles<CustomerCreated>>().ImplementedBy<DomainEventHandle<CustomerCreated>>().LifestyleSingleton());
            container.Register(Component.For<Handles<ProductCodeCreated>>().ImplementedBy<DomainEventHandle<ProductCodeCreated>>().LifestyleSingleton());
            container.Register(Component.For<Handles<ProductCreated>>().ImplementedBy<DomainEventHandle<ProductCreated>>().LifestyleSingleton());
            container.Register(Component.For<Handles<CountryTaxCreated>>().ImplementedBy<DomainEventHandle<CountryTaxCreated>>().LifestyleSingleton());

            DomainEvents.Init(container);
        }
    }
}