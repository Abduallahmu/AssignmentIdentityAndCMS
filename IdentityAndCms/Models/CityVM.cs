using System.ComponentModel.DataAnnotations;

namespace IdentityAndCms.VM
{
    public class CityVM
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int CountryId { get; set; }




    }
}
