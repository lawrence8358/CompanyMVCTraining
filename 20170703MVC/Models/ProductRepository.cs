using System;
using System.Linq;
using System.Collections.Generic;

namespace _20170703MVC.Models
{
    public class ProductRepository : EFRepository<Product>, IProductRepository
    {
        public IQueryable<Product> 取得前10筆資料()
        {
            return this.All().Take(10);
        }

        public Product Find(int id)
        {
            return this.All().FirstOrDefault(p => p.ProductId == id);
        }
    }

    public interface IProductRepository : IRepository<Product>
    {

    }
}