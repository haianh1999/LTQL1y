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
        StringProcess strPro = new StringProcess();
        private string encrytionpass;


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
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)

        {
            if (CheckSession() == 1)

            {

                return RedirectToAction("Index", "HomeAdmin", new { Area = "Admins" });
            }
            else if (CheckSession() == 2)

            {
                return RedirectToAction("Index", "HomeEmp", new { Area = "Employees" });

            }
            ViewBag.ReturnUrl = returnUrl;
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login(Account acc)
        {
            if (ModelState.IsValid)
            {
                var passToMD5 = strPro.GetMD5((string)acc.PassWord);
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
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(Account acc, string returnUrl)
        {
            try
            {
                if (!string.IsNullOrEmpty((string)acc.UserName) && !string.IsNullOrEmpty(acc.Password))
                {
                    using (var db = new LaptrinhquanlyDBcontext())
                    {
                        object ecry = null;
                        var passToMD5 = strPro.GetMD5((string)acc.PassWord);
                        var account = db.Accounts.Where(m => m.UserName.Equals(acc.UserName) && m.Password.Equals(passToMD5)).Count();
                        if (account == 1)
                        {
                            FormsAuthentication.SetAuthCookie((string)acc.UserName, false);
                            Session["idUser"] = acc.UserName;
                            Session["roleUser"] = acc.RoleID;
                            return RedirectTolocal(returnUrl);
                        }
                        ModelState.AddModelError("", "Thông tin đăng nhập chưa chính xác");
                    }
                }

                ModelState.AddModelError("", "Username and password is required.");
            }
            catch
            {
                ModelState.AddModelError("", "Hệ thống đang được bảo trì, vui lòng liên hệ với quản trị viên");
            }
            return View(acc);
        }
        private ActionResult RedirectTolocal(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || returnUrl == "/")
            {
                if (CheckSession() == 1)
                {
                    return RedirectToAction("Index", "HomeAdmin", new { Area = "Admins" });
                }
                else if (CheckSession() == 2)
                {
                    return RedirectToAction("Index", "HomeEmp", new { Area = "Employees" });
                }
            }
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        //Kiểm tra người dùng đăng nhập quyền gì
        private int CheckSession()
        {
            using (var db = new LaptrinhquanlyDBcontext())
            {
                var user = HttpContext.Session["idUser"];

                if (user != null)
                {
                    var role = db.Accounts.Find(user.ToString()).RoleID;

                    if (role != null)
                    {
                        if (role.ToString() == "Admin")

                        {
                            return 1;
                        }

                        else if (role.ToString() == "nv")

                        {
                            return 2;
                        }
                    }
                }
            }
            return 0;
        }
    }
}