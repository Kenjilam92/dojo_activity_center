using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dojo_activity.Models
{
    public class User
    {
        [Key]
        public int UserId {get;set;}
        [Required]
        [MinLength(2,ErrorMessage="Name with a minimum length of 2 characters")]
        public string Name {get;set;}
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email {get;set;}
        [Required]
        [DataType(DataType.Password)]
        [MinLength(8,ErrorMessage="Password must be at least 8 characters")]
        [RegexPass]
        public string Password {get;set;}
        public DateTime CreateAt {get;set;} = DateTime.Now ;
        public DateTime UpdateAt {get;set;} = DateTime.Now ;
        //////
        [NotMapped]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string CFPass {get;set;}
        //////
        public List<Associate> AllActs {get;set;}
        
    }
}