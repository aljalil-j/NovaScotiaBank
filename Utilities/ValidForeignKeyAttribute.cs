using NovaScotia.Data;
using JPSBillPayment.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NovaScotia.Utilities
{
    public class ValidForeignKeyAttribute: ValidationAttribute
    {
        

        protected override ValidationResult IsValid(
            object value, ValidationContext validationContext)
        {
            var _context = (BillContext)validationContext.GetService(typeof(BillContext));
            var entity = _context.Users.SingleOrDefault(e => e.premisesNumber.ToString() == value.ToString());
            //Customer result = userManager.Users.SingleOrDefault(e=> e.premisesNumber.ToString() == value.ToString());

            if (entity == null)
            {
                return new ValidationResult(GetErrorMessage(value.ToString()));
            }
            return ValidationResult.Success;
        }

        public string GetErrorMessage(string premNum)
        {
            return $"Premises Number {premNum} is Not Registered.";
        }
    }
}
