namespace _20170703MVC.Models
{
    using _20170703MVC.Models.InputValidations;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    
    [MetadataType(typeof(ProductMetaData))]
    public partial class Product
    {
    }
    
    public partial class ProductMetaData
    {
        [Required]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "必填欄位 {0} 是必要輸入的")]
        [DisplayName("商品名稱")] //有兩個多形
        [StringLength(80, ErrorMessage="欄位長度不得大於 80 個字元")]
        [ValidateTaiwanSIDAttribute]
        public string ProductName { get; set; }

        [Required]
        [DisplayName("商品價格")]
        [DisplayFormat(DataFormatString = "{0:0}")] //Net StringFormate
        public Nullable<decimal> Price { get; set; }

        [Required]
        [DisplayName("是否上架")]
        public Nullable<bool> Active { get; set; }

        [Required]
        [DisplayName("商品庫存")]
        public Nullable<decimal> Stock { get; set; }

        public virtual ICollection<OrderLine> OrderLine { get; set; }
    }
}
