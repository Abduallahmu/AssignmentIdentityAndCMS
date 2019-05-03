using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IdentityAndCms.CMS
{
    public class Country
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public List<City> Cities { get; set; }

        public List<Person> People { get; set; }

    }
}
