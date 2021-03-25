using NovaScotia.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NovaScotia.Utilities
{
    public class UniqueCusNum:ValidationAttribute
    {
        protected override ValidationResult IsValid(
            object value, ValidationContext validationContext)
        {
            var _context = (BillContext)validationContext.GetService(typeof(BillContext));
            var entity = _context.Users.SingleOrDefault(e => e.Id == value.ToString());

            if (entity == null)
            {
                return new ValidationResult(GetErrorMessage(value.ToString()));
            }
            return ValidationResult.Success;
        }

        public string GetErrorMessage(string premNum)
        {
            return $"Customer ID {premNum} is not Registered.";
        }
    }
}
