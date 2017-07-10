using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Styler.Areas.Admin.Models
{
    public class NavBarViewModel
    {
        public List<NavItem> NavItems { get; set; }
    }
    public class NavItem
    {
        public string ItemName { get; set; }
        public string Url { get; set; }
    }
}