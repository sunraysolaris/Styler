using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Styler.Areas.Admin.Models.Categories
{
    public class CategoriesViewModel : BaseViewModel
    {
        public int CategoryID { get; set; }
        [DisplayName("დასახელება")]
        public string CategoryName { get; set; }
        [DisplayName("დასახელება ინგლისურად")]
        public string CategoryNameEng { get; set; }
        [DisplayName("დასახელება რუსულად")]
        public string CategoryNameRus { get; set; }
    }
}