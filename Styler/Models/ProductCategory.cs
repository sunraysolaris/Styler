using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Styler.Models
{
    public class ProductCategory
    {
        [Key]
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string CategoryNameEng { get; set; }
        public string CategoryNameRus { get; set; }
        //public ICollection<Product> Products { get; set; }
    }
}