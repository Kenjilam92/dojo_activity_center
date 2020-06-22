using System.ComponentModel.DataAnnotations;

namespace dojo_activity.Models
{
    public class LoginUser
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string LoginEmail {get;set;}
        [Required]
        [DataType(DataType.Password)]
        [MinLength(8,ErrorMessage="Password need to be at least 8 charcters")]
        public string LoginPassword {get;set;} 
    }
}