using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using NovaScotia.Models;
using NovaScotia.Data;

namespace NovaScotia.Utilities
{
    public class ValidUniqueValueAttribute: ValidationAttribute
    {
        protected override ValidationResult IsValid(
            object value, ValidationContext validationContext)
        {
            var _context = (BillContext)validationContext.GetService(typeof(BillContext));
            var entity = _context.Users.SingleOrDefault(e => e.premisesNumber.ToString() == value.ToString());

            if (entity != null)
            {
                return new ValidationResult(GetErrorMessage(value.ToString()));
            }
            return ValidationResult.Success;
        }

        public string GetErrorMessage(string premNum)
        {
            return $"Premises Number {premNum} is already in use.";
        }
    }
}
