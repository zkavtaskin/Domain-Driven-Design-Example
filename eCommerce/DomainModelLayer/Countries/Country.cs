using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eCommerce.Helpers.Domain;

namespace eCommerce.DomainModelLayer.Countries
{
    public class Country : IAggregateRoot
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }

        public static Country Create(string name)
        {
            return Create(Guid.NewGuid(), name);
        }

        public static Country Create(Guid id, string name)
        {
            Country country = new Country()
            {
                Id = id,
                Name = name
            };

            DomainEvents.Raise<CountryCreated>(new CountryCreated() { Country = country });

            return country;
        }
    }
}
