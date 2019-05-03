using IdentityAndCms.CMS;
using IdentityAndCms.Interface;
using IdentityAndCms.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace IdentityAndCms.Service
{
    public class CityService : ICityService
    {
        private readonly CmsDbContext _CmsDbContext;

        public CityService(CmsDbContext CmsDbContext)
        {
            _CmsDbContext = CmsDbContext;
        }

        public List<City> AllCities()
        {
            return _CmsDbContext.Cities.ToList();
        }

        public City CreateCity(City city, int countryId)
        {
            var name = _CmsDbContext.Countries
                .Include(cities => cities.Cities)
                .ThenInclude(people => people.People)
                .SingleOrDefault(countryid => countryid.Id == countryId);

            name.Cities.Add(city);

            _CmsDbContext.SaveChanges();
            return city;
        }

        public bool DeleteCity(int id)
        {
            bool wasRemoved = false;

            City city = _CmsDbContext.Cities
                .Include(people => people.People)
                .SingleOrDefault(get => get.Id == id);

            if (city == null)
            {
                return wasRemoved;
            }

            _CmsDbContext.Cities.Remove(city);
            _CmsDbContext.SaveChanges();
            return wasRemoved;
        }

        public City FindCity(int id)
        {
            if (id == 0)
            {
                return null;
            }

            var city = _CmsDbContext.Cities
                .Include(people => people.People)
                .Include(country => country.Country)
                .SingleOrDefault(get => get.Id == id);

            return city;

        }

        public bool UpdateCity(City city)
        {
            bool wasUpdate = false;
            City ok = _CmsDbContext.Cities
                .Include(people => people.People)
                .SingleOrDefault(save => save.Id == city.Id);
            {
                if (ok != null)
                {
                    ok.Name = city.Name;

                    _CmsDbContext.SaveChanges();
                    wasUpdate = true;
                }
            }
            return wasUpdate;
        }
    }
}
