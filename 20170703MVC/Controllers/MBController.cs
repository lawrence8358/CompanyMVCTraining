using _20170703MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _20170703MVC.Controllers
{
    public class MBController : Controller
    {
        // GET: MB
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductEdit(int id)
        {
            ProductRepository reop = RepositoryHelper.GetProductRepository();
            ViewData.Model = reop.Find(id);

            return View();
        }

        [HttpPost]
        public ActionResult ProductEdit(int id, Product product)
        {
            //多個參數Binding會依照順序依序繫結
            //可參考http://blog.miniasp.com/post/2015/11/08/ASPNET-MVC-Developer-Note-Part-25-Value-Provider-and-Model-Binder.aspx 
            return Json(product);
        }
    }
}