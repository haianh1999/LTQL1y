using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LTQL11.Controllers
{
    public class HomeController : Controller
    {
        // Đối với action cho phép truy cập không cần kiểm tra đăng nập thì viết
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
        //Muố kiểm tra đăng nhập trước khi truy cập action nào thì viết
        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}