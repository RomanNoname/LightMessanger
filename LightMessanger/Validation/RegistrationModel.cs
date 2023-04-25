using System.ComponentModel.DataAnnotations;

namespace LightMessanger.WEB.Validation
{
    public class RegistrationModel
    {
        [Required(ErrorMessage = "Enter your email address")]
        [EmailAddress(ErrorMessage = "Please provide your current email address")]
        public string Email { get; set; }



        [Required(ErrorMessage = "Enter your login")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The name must be from 3 to 50 characters")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Enter your password")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "The password must be from 3 to 20 characters")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{3,20}$", ErrorMessage = "The password must contain at least a letter, lowercase and uppercase, and at least one digit")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
