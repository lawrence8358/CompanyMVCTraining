using System;
using System.ComponentModel.DataAnnotations;

namespace _20170703MVC.Models
{
    public class ProductBatchView
    {
        [Required]
        public int ProductId { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<bool> Active { get; set; }
    }
}