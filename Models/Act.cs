using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace dojo_activity.Models
{
    public class Act
    {
        [Key]
        public int ActId {get;set;}
        [Required]
        public string Title {get;set;}
        [Required]
        [Future]
        [DataType(DataType.Date)]
        public DateTime Date {get;set;}
        [Required]
        [DataType(DataType.Time)]
        public DateTime Time {get;set;}
        [Required]
        [Range(0,Int32.MaxValue)]
        public int Duration {get;set;}
        [Required]
        public string DurationType {get;set;}
        [Required]
        public string Description {get;set;}
        public DateTime CreateAt {get;set;} = DateTime.Now ;
        public DateTime UpdateAt {get;set;} = DateTime.Now ;
        /////
        [Required]
        public int UserId {get;set;}
        public User Creater {set;get;}
        public List<Associate> AllParties {get;set;}




    }
}