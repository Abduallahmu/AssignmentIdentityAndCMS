using IdentityAndCms.CMS;
using IdentityAndCms.Interface;
using IdentityAndCms.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace IdentityAndCms.Service
{
    public class CountryService : ICountryService
    {
        private readonly CmsDbContext _CmsDbContext;

        public CountryService(CmsDbContext DbContext)
        {
            _CmsDbContext = DbContext;
        }

        public List<Country> AllCountry()
        {
            return _CmsDbContext.Countries
                .Include(cities => cities.Cities)
                .ThenInclude(people => people.People)
                .ToList();
        }

        public Country CreateCountry(string name)
        {
            Country country = new Country()
            {
                Name = name
            };

            _CmsDbContext.Countries.Add(country);
            _CmsDbContext.SaveChanges();
            return country;
        }

        public bool DeleteCountry(int id)
        {
            bool wasRemoved = false;

            Country country = _CmsDbContext.Countries
                .Include(cities => cities.Cities)
                .ThenInclude(people => people.People)
                .SingleOrDefault(save => save.Id == id);

            if (country == null)
            {
                return wasRemoved;
            }

            _CmsDbContext.Countries.Remove(country);
            _CmsDbContext.SaveChanges();
            return wasRemoved;
        }

        public Country FindCountry(int id)
        {
            return _CmsDbContext.Countries
                .Include(cities => cities.Cities)
                .ThenInclude(people => people.People)
                .SingleOrDefault(save => save.Id == id);
        }

        public bool UpdateCountry(Country country)
        {
            bool wasUpdate = false;
            Country stud = _CmsDbContext.Countries
                .Include(cities => cities.Cities)
                .ThenInclude(people => people.People)
                .SingleOrDefault(teachers => teachers.Id == country.Id);
            {
                if (stud != null)
                {
                    stud.Name = country.Name;

                    _CmsDbContext.SaveChanges();
                    wasUpdate = true;
                }
            }
            return wasUpdate;
        }
    }
}
