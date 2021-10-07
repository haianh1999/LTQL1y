using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LTQL11.Models;

namespace LTQL11.Controllers
{
    public class EmployeesController : Controller
    {
        private LaptrinhquanlyDBcontext db = new LaptrinhquanlyDBcontext();
        AutogenerateKey auKey = new AutogenerateKey();

        // GET: Employees
        public ActionResult Index()
        {
            return View(db.Employees.ToList());
        }

        // GET: Employees/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            string NewID = "";
            var emp = db.Persons.ToList().OrderByDescending(c => c.PersonID);
            var countEmployee = db.Persons.Count();

            if (countEmployee == 0)
            {
                NewID = "PS001";
            }
            else
            {
                NewID = auKey.GenerateKey(emp.FirstOrDefault().PersonID);
            }
            ViewBag.newPerID = NewID;
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
   

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
