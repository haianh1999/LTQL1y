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
    public class PeopleController : Controller
    {
        //khai bao dbcontext de lam viec voi database
        private LaptrinhquanlyDBcontext db = new LaptrinhquanlyDBcontext();
        AutogenerateKey auKey = new AutogenerateKey();

        // GET: People
        public ActionResult Index()
        {
            //tra ve view index kem theo list danh sach pesron trong database
            return View(db.Persons.ToList());
        }

        // GET: People/Details/5
        public ActionResult Details(string id)
        {
            // neu id chuyen vao=null thi tra ve trang loi
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //tim kiem person theo id duoc gui len
            Person person = db.Persons.Find(id);
            if (person == null)
            {
                //tra ve trang not found neu ko tim thay du lieu
                return HttpNotFound();
            }
            // neu tim thay du lieu tra ve view kem theo thông tin person
            return View(person);
        }

        // GET: People/Create
        public ActionResult Create()
        {
            string NewID = "";
            var ps = db.Persons.ToList().OrderByDescending(c => c.PersonID);
            var countPS = db.Persons.Count();

            if (countPS == 0)
            {
                NewID = "PS001";
            }
            else
            {
                NewID = auKey.GenerateKey(ps.FirstOrDefault().PersonID);
            }
            ViewBag.newPerID = NewID;

            //tra ve view de cho ng dug nhap thong tin
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        // quan ly phien lam viec giua client va sever
        [ValidateAntiForgeryToken]
        // nhan gia tri cac thuoc tinh tu client gui len
        public ActionResult Create( Person person)
        {
            //neu thoa man du lieu rang buoc
            if (ModelState.IsValid)
            {
                // add doi tượng gửi lên tu phia client va dbcontext
                db.Persons.Add(person);
                //luu thay doi vao database
                db.SaveChanges();
                //dieu huong ve action index
                return RedirectToAction("Index");
            }
            //giu nguyen view create kem thông bao loi
            return View(person);
        }

        // GET: People/Edit/5
        public ActionResult Edit(string id)
        {
            var ps = db.Persons.Select(p => p).Where(p => p.PersonID == id).FirstOrDefault();
            return View(ps);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Person person)
        {
            if (ModelState.IsValid)
            {
                db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
                    return View();
        }

        // GET: People/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.Persons.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Person person = db.Persons.Find(id);
            db.Persons.Remove(person);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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
