using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace pass.Models
{
    public class Belt : BaseEntity
    {
        public int beltid {get;set;}
        public string color {get;set;}
        public string description {get;set;}
        public int studentid {get;set;}
        public Student student {get;set;}
        public List<Success> beltsachieved {get;set;}
        public Belt(){
            beltsachieved = new List<Success>();
        }

    }

}