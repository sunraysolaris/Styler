using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Styler.Areas.Admin.Models
{
    public class ProductsListViewModel
    {
        public int? PageCount { get; set; }
        public int CurrentPageNumber { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }
}