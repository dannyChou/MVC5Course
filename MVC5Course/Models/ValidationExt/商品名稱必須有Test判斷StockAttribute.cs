using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.ModelBinding;

namespace MVC5Course.Models.ValidationExt
{
    public class 商品名稱必須有Test判斷StockAttribute : ValidationAttribute
    {

        public 商品名稱必須有Test判斷StockAttribute(string otherProperty)
            : base("{0} 必須有Test 當 {1} 的數量為0時")
        {
            if (otherProperty == null)
            {
                throw new ArgumentNullException("otherProperty");
            }
            OtherProperty = otherProperty;
        }

        public string OtherProperty { get; private set; }

        public string OtherPropertyDisplayName { get; internal set; }

        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, OtherPropertyDisplayName ?? OtherProperty);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            PropertyInfo otherPropertyInfo = validationContext.ObjectType.GetProperty(OtherProperty);
            if (otherPropertyInfo == null)
            {
                return new ValidationResult("沒指定數量欄位");
            }

            object otherPropertyValue = otherPropertyInfo.GetValue(validationContext.ObjectInstance, null);
            if (otherPropertyValue.ToString().Equals("0"))
            {
                if (!value.ToString().Contains("Test"))
                {
                    OtherPropertyDisplayName = ModelMetadataProviders.Current.GetMetadataForProperty(() => validationContext.ObjectInstance, validationContext.ObjectType, OtherProperty).GetDisplayName();
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                }
            }
            return null;
        }
    }
}