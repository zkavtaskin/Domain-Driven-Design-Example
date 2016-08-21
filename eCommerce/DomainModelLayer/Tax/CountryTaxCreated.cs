using eCommerce.Helpers.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eCommerce.DomainModelLayer.Tax
{
    public class CountryTaxCreated : DomainEvent
    {
        public CountryTax CountryTax { get; set; }

        public override void Flatten()
        {
            this.Args.Add("CountryTaxId", CountryTax.Id);
            this.Args.Add("CountryTaxCountryId", CountryTax.Country.Id);
            this.Args.Add("CountryTaxPercentage", this.CountryTax.Percentage);
            this.Args.Add("CountryTaxType", this.CountryTax.Type);
        }
    }
}
