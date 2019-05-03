using Microsoft.AspNetCore.Identity;

namespace IdentityAndCms.Models
{
    public class User : IdentityUser
    {
        public  int Age { get; set; }
    }
}
