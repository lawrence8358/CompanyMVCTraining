using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using _20170724WebApi.Models;

namespace _20170724WebApi.Controllers
{
    /// <summary>
    /// 商品資料
    /// </summary>
    public class ProductsController : ApiController
    {
        private FabricsEntities db = new FabricsEntities();

        public ProductsController()
        {
            db.Configuration.LazyLoadingEnabled = false; //關閉延遲載入，避免循環參考的問題
        }

        /// <summary>
        /// 取得商品清單
        /// </summary>
        /// <returns></returns>
        // GET: api/Products
        public IQueryable<Product> GetProduct()
        {
            return db.Product;
        }

        // GET: api/Products/5
        /// <summary>
        /// 取得單筆商品資料
        /// </summary>
        /// <param name="id">商品識別碼</param>
        /// <returns></returns>
        [ResponseType(typeof(Product))]
        public IHttpActionResult GetProduct(int id)
        {
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // PUT: api/Products/5
        /// <summary>
        /// 更新商品資料
        /// </summary>
        /// <param name="id">商品識別碼</param>
        /// <param name="product">商品Model</param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProduct(int id, Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.ProductId)
            {
                return BadRequest();
            }

            db.Entry(product).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Products
        /// <summary>
        /// 新增商品資料
        /// </summary>
        /// <param name="product">商品Model</param>
        /// <returns></returns>
        [ResponseType(typeof(Product))]
        public IHttpActionResult PostProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Product.Add(product);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = product.ProductId }, product);
        }

        // DELETE: api/Products/5
        /// <summary>
        /// 刪除商品資料
        /// </summary>
        /// <param name="id">商品識別碼</param>
        /// <returns></returns>
        [ResponseType(typeof(Product))]
        public IHttpActionResult DeleteProduct(int id)
        {
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            db.Product.Remove(product);
            db.SaveChanges();

            return Ok(product);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(int id)
        {
            return db.Product.Count(e => e.ProductId == id) > 0;
        }
    }
}