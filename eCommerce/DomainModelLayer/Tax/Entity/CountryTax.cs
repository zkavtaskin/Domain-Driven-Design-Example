using eCommerce.DomainModelLayer.Countries;
using eCommerce.Helpers.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eCommerce.DomainModelLayer.Tax
{
    public class CountryTax : IDomainEntity
    {
        public virtual Guid Id { get; protected set; }
        public virtual Country Country { get; protected set; }
        public virtual decimal Percentage { get; protected set; }
        public virtual TaxType Type { get; protected set; }

        public static CountryTax Create(TaxType type, Country country, decimal percentage)
        {
            return new CountryTax()
            {
                Id = Guid.NewGuid(),
                Country = country,
                Percentage = percentage,
                Type = type
            };
        }

        public static CountryTax Create(Guid id, TaxType type, Country country, decimal percentage)
        {
            CountryTax countryTax = Create(type, country, percentage);
            countryTax.Id = id;
            return countryTax;
        }
    }
}
