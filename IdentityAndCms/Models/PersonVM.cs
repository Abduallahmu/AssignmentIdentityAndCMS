using IdentityAndCms.CMS;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IdentityAndCms.VM
{
    public class PersonVM
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Age { get; set; }

        public int CityId { get; set; }

        public List<City> Cities = new List<City>();
    }
}
