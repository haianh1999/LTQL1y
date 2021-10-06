using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using LTQL11.Models;

namespace LTQL11.Controllers
{
    public class AccountController : Controller
    {
        Encrytion encry = new Encrytion();
        private LaptrinhquanlyDBcontext db = new LaptrinhquanlyDBcontext();
        // GET: Account
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Register(Account acc)
        {
            if (ModelState.IsValid)
            {
                //ma hoa mật khẩu trước khi lưu vào database
                acc.Password = encry.PasswordEncrytion(acc.Password);
                db.Accounts.Add(acc);
                db.SaveChanges();
                return RedirectToAction("Login", "Account");
            }
            return View(acc);
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login(Account acc)
        {
            if (ModelState.IsValid)
            {
                string encrytionpass = encry.PasswordEncrytion(acc.Password);
                var model = db.Accounts.Where(m => m.UserName == acc.UserName && m.Password == encrytionpass).ToList().Count();
                //thông tin đăng nhập chính xác
                if (model == 1)
                {
                    FormsAuthentication.SetAuthCookie((string)acc.UserName, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Thông tin đăng nhập không chính xác");
                }
            }
            return View(acc);
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");

        }
        private ActionResult RedirectToLocal(string returnUrl)
        {
          if (Url.IsLocalUrl(returnUrl))
          {
               return Redirect(returnUrl);
          }
          else
          {
             return RedirectToAction("Index", "Home");
          }
        }
    }
}