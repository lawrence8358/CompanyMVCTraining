using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _20170703MVC.Controllers
{
    public class ARController : Controller
    {
        // GET: AR
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Index2()
        {
            return PartialView("Index");
        }

        public ActionResult Content1()
        {
            return Content("測試Big5", "text/plain", System.Text.Encoding.GetEncoding("big5")); //上面的寫法等價底下的Content2，但編碼不同
        }

        public string Content2()
        {
            return "測試Unicode";
        }
    }
}