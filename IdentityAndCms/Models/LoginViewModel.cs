using System.ComponentModel.DataAnnotations;

namespace IdentityAndCms.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(80, MinimumLength = 8, ErrorMessage = "Password must be 8 long and maximum 20 long.")]
        public string Password { get; set; }
    }
}
