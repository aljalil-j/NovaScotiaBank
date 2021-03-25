using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NovaScotia.Models
{
    public class ScotiaTransaction
    {
        [Required]
       public int Id { get; set; }

        [Display(Name = "Account Number")]
        [Required]
        // [ValidUniqueValue]
        [StringLength(60, MinimumLength = 3)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "must be numeric")]
        public String AccountNumber { get; set; }

        public string TransactionType { get; set; }

        [Display(Name = "Amount")]
        [DataType(DataType.Currency)]
        [Required]
        public decimal Amount { get; set; }
    }
}
