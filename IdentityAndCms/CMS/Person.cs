using System.ComponentModel.DataAnnotations;

namespace IdentityAndCms.CMS
{
    public class Person
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Age { get; set; }

        public City City { get; set; }
    }
}
