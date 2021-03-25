using NovaScotia.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NovaScotia.Utilities.ScotiaUtilities
{
    public class AccountType:ValidationAttribute
    {
        protected override ValidationResult IsValid(
           object value, ValidationContext validationContext)
        {
            var _context = (ScotiaCustomerContext)validationContext.GetService(typeof(ScotiaCustomerContext));

            if(value.ToString() != "Savings" && value.ToString() != "Credit Card" && value.ToString() != "savings")
            {
                
                return new ValidationResult(GetErrorMessage(value.ToString()));
            }



           


            return ValidationResult.Success;
        }

        public string GetErrorMessage(string premNum)
        {
           
            return $"Account {premNum} is invalid, Must be Savings or Credit Card ";
        }
    }
}
