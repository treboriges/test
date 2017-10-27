using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pass.Models
{
    public abstract class BaseEntity
    {
    }
    public class Student
    {
        public int studentid { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }
        [Required]
        [MinLength(8, ErrorMessage = "password needs to be at least 8 characters")]
        public string password { get; set; }
        [Required]
        [Compare("password", ErrorMessage="Passwords do not match")]
        [NotMapped]
        public string c_password {get;set;}
        public List<Belt> createdbelt {get;set;}
        public List<Success> passedbelt {get;set;}
        public Student(){
            createdbelt = new List<Belt>();
            passedbelt = new List<Success>();

        }
        
    }
}