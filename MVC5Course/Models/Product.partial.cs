namespace MVC5Course.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using ValidationExt;

    [MetadataType(typeof(ProductMetaData))]
    public partial class Product:IValidatableObject
    {
        public int 訂單數量 { get {
                return this.OrderLine.Count();
                //return this.OrderLine.Count(p => p.Qty > 15);
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.Price > 100 && this.Stock > 5) {
                yield return new ValidationResult("價格與庫存數量不合理",new string[] { "price","stock"});
            }

            if (this.OrderLine.Count() == 0 && this.ProductName.Contains("X")) {
                yield return new ValidationResult("Stock與訂單數量不合", new string[] { "ProductName" });
            }
            yield break;
        }
    }
    
    public partial class ProductMetaData
    {
        [Required(ErrorMessage = "請輸入商品名稱")]
        //[MinLength(3), MaxLength(30)]
        //[RegularExpression("(.+)-(.+)", ErrorMessage = "商品名稱格式錯誤")]
        [DisplayName("商品名稱")]
        [商品名稱必須有Danny(ErrorMessage = "商品名稱必須有Danny",DannyType = "Y")]
        [商品名稱必須有Test判斷Stock("Stock")]
        public string ProductName { get; set; }
        [Required]
        [Range(0, 999999, ErrorMessage = "請設定正確的商品價格範圍")]
        [DisplayFormat(DataFormatString = "{0:0}", ApplyFormatInEditMode = true)]
        [DisplayName("商品價格")]
        public Nullable<decimal> Price { get; set; }
        [Required]
        [DisplayName("是否上架")]
        public Nullable<bool> Active { get; set; }
        [Required]
        //[Range(0, 100, ErrorMessage = "請設定正確的商品庫存數量")]
        [DisplayName("商品庫存")]
        public Nullable<decimal> Stock { get; set; }
    }
}
