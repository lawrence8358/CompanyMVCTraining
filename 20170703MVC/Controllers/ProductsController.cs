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
    public class ProductsController : Controller
    {
        //private FabricsEntities db = new FabricsEntities();
        ProductRepository _product = RepositoryHelper.GetProductRepository(); //改由Repository來操作

        // GET: Products
        public ActionResult Index()
        {
            //return View(db.Product.Take(10));
            //改由Repository來操作
            return View(_product.取得前10筆資料());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Product product = db.Product.Find(id);
            //改由Repository來操作
            Product product = _product.Find(id.Value);

            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                //db.Product.Add(product);
                //db.SaveChanges();
                //改由Repository來操作
                _product.Add(product);
                _product.UnitOfWork.Commit();

                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Product product = db.Product.Find(id);
            //改由Repository來操作
            Product product = _product.Find(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        public ActionResult Edit(int id, FormCollection form)
        {
            //此處的參數form沒有任何意義，只是為了要讓修改的Action多型跟Get不一樣 
            var product = _product.Find(id);
            //if (TryUpdateModel(product, new string[] { "ProductId", "ProductName", "Price", "Active", "Stock" })) //延遲資料binding
            //{
            //    _product.UnitOfWork.Commit();
            //    return RedirectToAction("Index");
            //}

            //不檢查Model正確性，故意讓程式發生錯誤
            TryUpdateModel(product, new string[] { "ProductId", "ProductName", "Price", "Active", "Stock" });
            _product.UnitOfWork.Commit();
            return RedirectToAction("Index");

            //if (ModelState.IsValid)
            //{
            //    //db.Entry(product).State = EntityState.Modified;
            //    //db.SaveChanges();
            //    //改由Repository來操作
            //    var db = _product.UnitOfWork.Context;
            //    db.Entry(product).State = EntityState.Modified;
            //    db.SaveChanges();

            //    return RedirectToAction("Index");
            //}
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Product product = db.Product.Find(id); 
            //改由Repository來操作
            Product product = _product.Find(id.Value);

            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Product product = db.Product.Find(id);
            //db.Product.Remove(product);
            //db.SaveChanges();
            //改由Repository來操作
            Product product = _product.Find(id);
            _product.Delete(product);
            _product.UnitOfWork.Commit();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
                //改由Repository來操作
                _product.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost]
        public ActionResult BatchUpdate(List<ProductBatchView> data)
        //public ActionResult BatchUpdate(ProductBatchView[] data)
        {
            if (ModelState.IsValid)
            {
                //ProductBatchView[] 此寫法在前端必須對應data[i].ProductId的格式，但在C# 6.0有問題
                //因此目前的解法式移除Microsoft.CodeDom.Providers.DotNetCompilerPlatform
                //解法可參考 http://haacked.com/archive/2008/10/23/model-binding-to-a-list.aspx/
                foreach (var item in data)
                {
                    //不需要檢查是否有異動，EF的機制會自動檢查
                    var product = _product.Find(item.ProductId);
                    product.Active = item.Active;
                    product.Price = item.Price;
                }

                _product.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            ViewData.Model = _product.取得前10筆資料();
            return View("Index");
        }
    }
}
