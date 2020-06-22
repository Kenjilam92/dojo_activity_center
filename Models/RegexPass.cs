using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace dojo_activity.Models
{
    public class RegexPassAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var reg = new Regex(@"^(?=.*?\d)(?=.*?[a-zA-Z])(?=.*?,!@#$%^&*+=)[A-Za-z\d,!@#$%^&*+=]{8,}$");
            if (!reg.IsMatch((string)value))
                return new ValidationResult("Password must have at least 1 letter, 1 number and 1 special character.");
            return ValidationResult.Success;
        }
    }
}