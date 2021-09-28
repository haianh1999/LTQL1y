using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LTQL11.Models
{
    public class Person
    {
        [AllowHtml]
        public string PersonID { get; set; }
        public string PersonName { get; set; }
    }
}