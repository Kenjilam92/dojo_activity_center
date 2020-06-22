using System.ComponentModel.DataAnnotations;

namespace dojo_activity.Models
{
    public class Associate
    {
        [Key]
        public int AssociateId {get;set;}
        [Required]
        public int UserId {get;set;}
        public User User {get;set;}
        [Required]
        public int ActId {get;set;}
        public  Act Act {get;set;}
    }
}