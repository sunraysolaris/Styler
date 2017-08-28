using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Styler.Areas.Admin.Models.Categories
{
    public class CategoriesListViewModel
    {
        public int? PageCount { get; set; }
        public int CurrentPageNumber { get; set; }
        public string KeyWord { get; set; }
        public List<CategoriesViewModel> Categories { get; set; }
    }
}