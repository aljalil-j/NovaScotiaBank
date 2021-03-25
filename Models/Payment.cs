using NovaScotia.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NovaScotia.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }

        [Display(Name = "Amount")]
        //[DataType(DataType.Currency)]
        [Required]
        public decimal amountPaid { get; set; }

        
        [Display(Name = "Customer ID")]
        [UniqueCusNum]
        [Required]
        public string customerId { get; set; }

        [Display(Name = "Card Holder Name")]
        [Required]
        public string Fname { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Please enter a valid integer")]
        [Display(Name = "Premises ID")]
        [ValidForeignKey]
        [Required]
        public int premisesNum { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Please enter a valid integer")]
        [Display(Name = "Card Number")]
        [Required]
        public int cardNumber { get; set; }
    }
}
