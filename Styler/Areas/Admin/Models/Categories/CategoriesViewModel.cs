using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Styler.Areas.Admin.Models.Categories
{
    public class CategoriesViewModel
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string CategoryNameEng { get; set; }
        public string CategoryNameRus { get; set; }
    }
}