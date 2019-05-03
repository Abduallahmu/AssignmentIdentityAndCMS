using IdentityAndCms.CMS;
using System.Collections.Generic;

namespace IdentityAndCms.Interface
{
    public interface ICountryService
    {
        Country CreateCountry(string name);

        List<Country> AllCountry();

        Country FindCountry(int id);

        bool UpdateCountry(Country country);

        bool DeleteCountry(int id);

    }
}
