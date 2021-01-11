using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace dojo_activity.Models
{
    public class RegexPassAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
<<<<<<< HEAD
            var reg = new Regex(@"^(?=.*?\d)(?=.*?[a-zA-Z])(?=.*?[!@#$%^&*+=])[A-Za-z\d,!@#$%^&*+=]{8,}$");
            if (value == null)
                return new ValidationResult("Password can't be emty");
            else if (!reg.IsMatch((string)value))
                return new ValidationResult("Password must have 8 characters, at least 1 letter, 1 number and 1 special character.");
=======
            var reg = new Regex(@"^(?=.*?\d)(?=.*?[a-zA-Z])(?=.*?[.!@#$%^&*+=])[A-Za-z\d,!@#$%^&*+=]{8,}$");
            if (!reg.IsMatch((string)value))
                return new ValidationResult("Password must have at least 1 letter, 1 number and 1 special character.");
>>>>>>> 1e9b810b6d30a923f23f03218ed8b451c2da8094
            return ValidationResult.Success;
        }
    }
}
