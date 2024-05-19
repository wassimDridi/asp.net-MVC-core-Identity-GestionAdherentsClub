using System.ComponentModel.DataAnnotations;

namespace GestionAdherentsClub.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Rememberme")]
        public bool RememberMe { get; set; }
    }
}
