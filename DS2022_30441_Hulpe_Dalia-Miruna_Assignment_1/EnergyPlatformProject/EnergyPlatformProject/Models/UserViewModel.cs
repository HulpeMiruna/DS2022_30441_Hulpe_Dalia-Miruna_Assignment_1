using System;
using System.ComponentModel.DataAnnotations;

namespace EnergyPlatformProject.Models
{
    public class UserViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Email required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Display(Name = "First name")]
        [Required(ErrorMessage = "First name required")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        [Required(ErrorMessage = "Last name required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Password required")]
        public string Password { get; set; }
    }
}
