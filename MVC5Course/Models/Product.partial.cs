using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models
{
	[MetadataType(typeof(ProductPartial))]
    public partial class Product
    {
    }

    public partial class ProductPartial
    {
        [Required(ErrorMessage = "請輸入商品名稱")]
        [RegularExpression("(.+)-(.+)", ErrorMessage = "格式錯誤")]
        public string ProductName { get; set; }
        [Required]
        [Range(0, 9999, ErrorMessage = "請設定正確的商品價格範圍")]
        public Nullable<decimal> Price { get; set; }
        [Required]
        public Nullable<bool> Active { get; set; }
        [Required]
        [Range(0, 100, ErrorMessage = "請設定正確的商品庫存量範圍")]
        public Nullable<decimal> Stock { get; set; }
    }
}