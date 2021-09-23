using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LTQL11.Models
{
    [Table("Students")]
    public class Student
    {
        [Key]
        public string StudentID { get; set; }

        public string StudentName { get; set; }
        public string Address { get; set; }
    }
}