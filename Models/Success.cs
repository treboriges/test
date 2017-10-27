using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace pass.Models
{
    public class Success : BaseEntity
    {
        public int successid {get;set;}
        public int studentid {get;set;}
        public Student student {get;set;}
        public int beltid {get;set;}
        public Belt belt {get;set;}
        public DateTime date {get;set;}
        public string description {get;set;}

    }
}