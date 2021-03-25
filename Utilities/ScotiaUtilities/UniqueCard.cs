using NovaScotia.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NovaScotia.Utilities.ScotiaUtilities
{
    public class UniqueCard: ValidationAttribute
    {
        protected override ValidationResult IsValid(
           object value, ValidationContext validationContext)
        {
            var _context = (ScotiaCustomerContext)validationContext.GetService(typeof(ScotiaCustomerContext));
            var entity = _context.ScotiaCustomer.SingleOrDefault(e => e.CardNumber == value.ToString());



            if (entity != null)
            {
                return new ValidationResult(GetErrorMessage(value.ToString()));
            }


            return ValidationResult.Success;
        }

        public string GetErrorMessage(string premNum)
        {
            return $"Card Number {premNum} is already in use.";
        }
    }
}
