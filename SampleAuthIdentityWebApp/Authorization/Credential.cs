using System.ComponentModel.DataAnnotations;

namespace SampleAuthWebApp.Authorization
{
    public class Credential
    {
        [Required]
        [Display(Description = "User Name")]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
