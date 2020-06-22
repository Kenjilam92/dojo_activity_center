using System;
using System.ComponentModel.DataAnnotations;

namespace dojo_activity.Models
{
    public class FutureAttribute: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if ( (DateTime)value < DateTime.Now )
                return new ValidationResult ("You can only add date from the future.");
            return ValidationResult.Success;
        }
    }
}