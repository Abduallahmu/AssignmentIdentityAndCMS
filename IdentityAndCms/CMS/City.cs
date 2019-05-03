using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IdentityAndCms.CMS
{
    public class City
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public Country Country { get; set; }

        public List<Person> People { get; set; }
    }
}
