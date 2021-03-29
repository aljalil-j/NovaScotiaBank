using NovaScotia.Utilities.ScotiaUtilities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NovaScotia.Models
{
    public class ScotiaCustomer
    {
        [Required]
        public int id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]

        public string Password { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        [Display(Name = "First Name")]
        public string First { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        [Display(Name = "Last Name")]
        public string Last { get; set; }



        [Required]
        [StringLength(60, MinimumLength = 3)]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Balance")]
        [DataType(DataType.Currency)]
        [Required]
        [Range(10000, 99999999999999999,ErrorMessage ="Balance must be at least $10,000")]
        public decimal Balance { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        [Display(Name = "Account Number")]
        [NumStart]
        [UniqueAccount]
        [RegularExpression("^[0-9]*$", ErrorMessage = "must be numeric")]
        public string AccountNumber { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        [Display(Name = "Card Number")]
        [CardStart]
        [UniqueCard]
        [RegularExpression("^[0-9]*$", ErrorMessage = "must be numeric")]
        public string CardNumber { get; set; }

        [Display(Name = "Available Balance")]
        [DataType(DataType.Currency)]
        [Required]
        [Range(10000, 99999999999999999, ErrorMessage = "Balance must be at least $10,000")]
        public decimal AvaBalance { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        [AccountType]
        [Display(Name = "Account Type")]
        public string AccountType { get; set; }

        public static implicit operator ScotiaCustomer(Customer v)
        {
            throw new NotImplementedException();
        }
    }
}
