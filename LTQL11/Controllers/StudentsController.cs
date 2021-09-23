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
        //tao action create tra ve vuew cho phep ng dung nhap thong tin de them moi
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student std)
        {
            if(ModelState.IsValid)
            {
                db.Students.Add(std);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }

}