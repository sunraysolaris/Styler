using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Styler.Areas.Admin.Models
{
    public class ProductsViewModel
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductNameEng { get; set; }
        public string ProductNameRus { get; set; }
        public string Description { get; set; }
        public string DescriptionEng { get; set; }
        public string DescriptionRus { get; set; }
        public decimal Price { get; set; }
        public string PhotoUrl { get; set; }
    }
}