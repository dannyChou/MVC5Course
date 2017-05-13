using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;

namespace MVC5Course.Models.ValidationExt
{
    public class 商品名稱必須有DannyAttribute : DataTypeAttribute
    {
        public string DannyType { get; set; }

        public 商品名稱必須有DannyAttribute() : base(DataType.Text)
        {

        }

        public override bool IsValid(object value)
        {
            if (DannyType.Equals("Y"))
            {
                var str = (string)value;
                return str.Contains("Danny");
            }
            else {
                return true;
            }
        }
    }
}