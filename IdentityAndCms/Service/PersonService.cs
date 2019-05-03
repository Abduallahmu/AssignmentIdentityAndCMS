using IdentityAndCms.CMS;
using IdentityAndCms.Interface;
using IdentityAndCms.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace IdentityAndCms.Service
{
    public class PersonService : IPersonService
    {
        Country Country = new Country();

        private readonly CmsDbContext _cmsDbContext;

        public PersonService(CmsDbContext cmsDbContext)
        {
            _cmsDbContext = cmsDbContext;
        }

        public List<Person> AllPersons()
        {
            return _cmsDbContext.People.ToList();
        }

        public Person CreatePersonWithoutCity(Person person)
        {
            if (person == null)
            {
                return null;
            }

            Person newPerson = new Person()
            {
                Name = person.Name,
                Age = person.Age,
            };

            if (newPerson != null)
            {
                _cmsDbContext.People.Add(newPerson);
                _cmsDbContext.SaveChanges();

                return newPerson;
            }

            return null;
        }

        public Person CreatePerson(Person person, int cityId)
        {
            var city = _cmsDbContext.Cities
                   .Include(people => people.People)
                   .SingleOrDefault(save => save.Id == cityId);

            city.People.Add(person);

            _cmsDbContext.People.Add(person);
            _cmsDbContext.SaveChanges();
            return person;
        }

        public bool DeletePerson(int id)
        {
            bool wasRemoved = false;

            Person person = _cmsDbContext.People.SingleOrDefault(save => save.Id == id);

            if (person == null)
            {
                return wasRemoved;
            }

            _cmsDbContext.People.Remove(person);
            _cmsDbContext.SaveChanges();
            return wasRemoved;
        }

        public Person FindPerson(int id)
        {
            return _cmsDbContext.People.SingleOrDefault(save => save.Id == id);
        }

        public Person FindPersonWithCity(int? id)
        {
            if (id != null)
            {
                return _cmsDbContext.People
                    .Include(city => city.City)
                    .SingleOrDefault(save => save.Id == id);
            }
            return null;
        }

        public bool UpdatePerson(Person person)
        {
            bool wasUpdate = false;
            Person stud = _cmsDbContext.People
                .SingleOrDefault(teachers => teachers.Id == person.Id);
            {
                if (stud != null)
                {
                    stud.Name = person.Name;
                    stud.Age = person.Age;

                    _cmsDbContext.SaveChanges();
                    wasUpdate = true;
                }
            }
            return wasUpdate;
        }

        public bool UpdatePersonWithCity(Person person, int? id)
        {
            bool wasUpdate = false;

            Person stud = _cmsDbContext.People
                .Include(city => city.City)
                .ThenInclude(country => country.Country)
                .SingleOrDefault(teachers => teachers.Id == person.Id);

            {
                if (stud != null)
                {
                    stud.Name = person.Name;
                    stud.Age = person.Age;

                    _cmsDbContext.SaveChanges();
                    wasUpdate = true;
                }
            }
            return wasUpdate;
        }
    }
}
