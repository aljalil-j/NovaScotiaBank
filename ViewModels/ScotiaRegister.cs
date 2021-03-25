using NovaScotia.Utilities;
using NovaScotia.Utilities.ScotiaUtilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NovaScotia.ViewModels
{
    public class ScotiaRegister
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


        [Display(Name = "Account Number")]
        [Required]
       // [UniqueAccount]
        [StringLength(60, MinimumLength = 3)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "must be numeric")]
        public String AccountNumber { get; set; }

        [Required]
        [DataType(DataType.Password)]

        public string Password { get; set; }
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and Confirm Password must match")]

        public string ConfirmPassword { get; set; }
    }

}
