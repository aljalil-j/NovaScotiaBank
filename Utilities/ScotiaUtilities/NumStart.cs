using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NovaScotia.Utilities.ScotiaUtilities
{
    public class NumStart: ValidationAttribute
    {
        protected override ValidationResult IsValid(
           object value, ValidationContext validationContext)
        {

            //var _context = (ScotiaCustomerContext)validationContext.GetService(typeof(ScotiaCustomerContext));
            string chk = value.ToString();
            
           

            if(chk.StartsWith("212") == false || chk.Length != 7)
            {
                
                return new ValidationResult(GetErrorMessage(value.ToString()));
            }

            return ValidationResult.Success;
        }

        public string GetErrorMessage(string premNum)
        {
           
            return $"Account {premNum} Must have 7 digits and begin with 212 ";
        }
    }
}
