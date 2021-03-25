using NovaScotia.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NovaScotia.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(60, MinimumLength = 3)]
        [Display(Name = "First Name")]
        public string Fname { get; set; }
        [Required]
        [StringLength(60, MinimumLength = 3)]
        [Display(Name = "Last Name")]
        public string Lname { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer")]
        [Display(Name = "Premises Number")]
        [Required]
        [ValidUniqueValue]
        public int premisesNumber { get; set; }

        [Required]
        [DataType(DataType.Password)]

        public string Password { get; set; }
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and Confirm Password must match")]

        public string ConfirmPassword { get; set; }
    }
}
