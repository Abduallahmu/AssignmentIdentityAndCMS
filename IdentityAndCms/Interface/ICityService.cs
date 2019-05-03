using IdentityAndCms.CMS;
using System.Collections.Generic;

namespace IdentityAndCms.Interface
{
    public interface ICityService
    {
        City CreateCity(City city, int countryId);

        List<City> AllCities();

        City FindCity(int id);

        bool UpdateCity(City city);

        bool DeleteCity(int id);

    }
}
