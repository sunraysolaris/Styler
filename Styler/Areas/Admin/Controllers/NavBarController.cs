﻿using Styler.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Styler.Areas.Admin.Controllers
{
    public class NavBarController : Controller
    {
        // GET: Admin/NavBar
        [ChildActionOnly]
        public ActionResult Sidebar()
        {
            var Items = new List<NavItem>();

            Items.Add(new NavItem { ItemName = "Customers", Url = "#" });
            Items.Add(new NavItem { ItemName = "Products", Url = "/Admin/Product/ProductList" });
            Items.Add(new NavItem { ItemName = "Categories", Url = "/Admin/Categories/CategoriesList" });
            Items.Add(new NavItem { ItemName = "Orders", Url = "#" });

            var Model = new NavBarViewModel
            {
                NavItems = Items
            };
            return PartialView("_SideNavBar", Model);
        }
        public ActionResult Header()
        {
            return PartialView("_Header");
        }
    }
}