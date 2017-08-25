using Styler.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Styler.Areas.Admin.Models
{
    public class ProductEditViewModel : BaseViewModel
    {
        public int ProductID { get; set; }
        [Required(ErrorMessage = "გთხოვთ შეიყვანოთ დასახელება!")]
        public string ProductName { get; set; }
        public string ProductNameEng { get; set; }
        public string ProductNameRus { get; set; }
        [Required(ErrorMessage = "გთხოვთ შეიყვანოთ აღწერა!")]
        public string Description { get; set; }
        public string DescriptionEng { get; set; }
        public string DescriptionRus { get; set; }
        [Required(ErrorMessage = "გთხოვთ შეიყვანოთ ფასი!")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Price { get; set; }
        public int[] CurrentCategories { get; set; }
        public IEnumerable<ProductCategory> CategoryList { get; set; }
        [Required(ErrorMessage = "გთხოვთ შეიყვანოთ ფოტოს ბმული!")]
        public string PhotoUrl { get; set; }
    }
}