using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eCommerce.Helpers.Repository;
using System.Linq.Expressions;
using eCommerce.Helpers.Specification;
using eCommerce.DomainModelLayer.Tax;

namespace eCommerce.DomainModelLayer.Customers.Spec
{
    public class CountryTypeOfTaxSpec : SpecificationBase<CountryTax>
    {
        readonly Guid countryId;
        readonly TaxType taxType;

        public CountryTypeOfTaxSpec(Guid countryId, TaxType taxType)
        {
            this.countryId = countryId;
            this.taxType = taxType;
        }

        public override Expression<Func<CountryTax, bool>> SpecExpression
        {
            get
            {
                return countryTax => countryTax.Country.Id == this.countryId
                    && countryTax.Type == this.taxType;
            }
        }

        public override bool Equals(object obj)
        {
            CountryTypeOfTaxSpec countryTypeOfTaxSpecCompare = obj as CountryTypeOfTaxSpec;
            if (countryTypeOfTaxSpecCompare == null)
                throw new InvalidCastException("obj");

            return countryTypeOfTaxSpecCompare.countryId == this.countryId &&
                countryTypeOfTaxSpecCompare.taxType == this.taxType;
        }
    }
}
