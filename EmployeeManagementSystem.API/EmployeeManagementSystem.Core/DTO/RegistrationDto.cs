using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Core.Dto
{
    public class RegistrationDto
    {

        [Required]
        [MaxLength(250, ErrorMessage = "First Name cannot have more than 250 characters")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(250, ErrorMessage = "Last Name cannot have more than 250 characters")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}