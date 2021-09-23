using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LTQL11.Models
{
    public class Employee: Person
    {
        public string Company { get; set; }
        public string Address { get; set; }
    }
}