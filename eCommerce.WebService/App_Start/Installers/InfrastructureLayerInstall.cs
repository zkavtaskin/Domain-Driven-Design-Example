using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using eCommerce.DomainModelLayer.Countries;
using eCommerce.DomainModelLayer.Customers;
using eCommerce.DomainModelLayer.Email;
using eCommerce.DomainModelLayer.Newsletter;
using eCommerce.DomainModelLayer.Products;
using eCommerce.DomainModelLayer.Tax;
using eCommerce.Helpers.Repository;
using eCommerce.InfrastructureLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eCommerce.WebService.App_Start.Installers
{
    public class InfrastructureLayerInstall : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IEmailDispatcher>().ImplementedBy<SmtpEmailDispatcher>().LifeStyle.Singleton);
            container.Register(Component.For<INewsletterSubscriber>().ImplementedBy<WSNewsletterSubscriber>().LifeStyle.Singleton);
            container.Register(Component.For<IEmailGenerator>().ImplementedBy<StubEmailGenerator>().LifeStyle.Singleton);

            container.Register(Component.For(typeof(IRepository<>), typeof(MemoryRepository<>)).ImplementedBy(typeof(MemoryRepository<>)).LifestyleSingleton());
            container.Register(Component.For<IUnitOfWork>().ImplementedBy<MemoryUnitOfWork>().LifestyleSingleton());

            container.Register(Component.For<IRepository<ProductCode>>().ImplementedBy<StubDataProductCodeRepository>().LifestyleSingleton());
            container.Register(Component.For<IRepository<Country>>().ImplementedBy<StubDataCountryRepository>().LifestyleSingleton());
            container.Register(Component.For<IRepository<CountryTax>>().ImplementedBy<StubDataCountryTaxRepository>().LifestyleSingleton());
            container.Register(Component.For<IRepository<Product>>().ImplementedBy<StubDataProductRepository>().LifestyleSingleton());
            container.Register(Component.For<ICustomerRepository>().ImplementedBy<StubDataCustomerRepository>().LifestyleSingleton());
            container.Register(Component.For<IDomainEventRepository>().ImplementedBy<MemDomainEventRepository>().LifestyleSingleton());
        }
    }
}