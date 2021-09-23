using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LTQL11.Models;

namespace LTQL11.Controllers
{
    public class StudentsController : Controller
    {
        LaptrinhquanlyDBcontext db = new LaptrinhquanlyDBcontext();
        // GET: Students
        public ActionResult Index()
        {
            var model = db.Students.ToList();
            return View(model);
        }
    }
}