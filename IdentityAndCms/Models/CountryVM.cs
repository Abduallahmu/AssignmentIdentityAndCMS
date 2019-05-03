using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IdentityAndCms.VM
{
    public class CountryVM
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string Age { get; set; }


    }
}
