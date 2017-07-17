using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using _20170703MVC.Models;

namespace _20170703MVC.Controllers
{
    public class OrderLinesController : Controller
    {
        private FabricsEntities db = new FabricsEntities();

        // GET: OrderLines
        public ActionResult Index(int id)
        {
            var orderLine = db.OrderLine.Where(p => p.ProductId.Equals(id)).Include(o => o.Order).Include(o => o.Product);
            return View(orderLine.Take(3).ToList());
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
