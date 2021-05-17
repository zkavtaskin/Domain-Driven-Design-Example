using eCommerce.DomainModelLayer.Countries;

namespace eCommerce.DomainModelLayer
{
    public class Settings
    {
        public virtual Country BusinessCountry { get; protected set; }

        public Settings()
        {
            
        }

        public Settings(Country businessCountry)
        {
            this.BusinessCountry = businessCountry;
        }
    }
}
