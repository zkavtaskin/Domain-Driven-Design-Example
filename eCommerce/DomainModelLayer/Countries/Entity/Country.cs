using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eCommerce.Helpers.Domain;

namespace eCommerce.DomainModelLayer.Countries
{
    public class Country : IDomainEntity
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }

        public static Country Create(string name)
        {
            return new Country()
            {
                Id = Guid.NewGuid(),
                Name = name
            };
        }

        public static Country Create(Guid id, string name)
        {
            Country country = Create(name);
            country.Id = id;
            return country;
        }
    }
}
