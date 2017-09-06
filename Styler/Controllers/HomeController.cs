using Styler.Areas.Admin.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Styler.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index(int categoryId = 0, string keyword = null) // TODO: maxPrice and Minprice to add in search filter
        {
            var productQuery = DbContext.Products.AsQueryable();

            if (categoryId > 0)
            {
                var category = DbContext.Categories.SingleOrDefault(c => c.CategoryID == categoryId);
                productQuery = productQuery.Where(p => p.ProductCategories.Contains(category));
            }

            var products = productQuery.Where(p => p.Description.Contains(keyword)
                                                     || p.ProductName.Contains(keyword)
                                                     || p.ProductNameEng.Contains(keyword)
                                                     || p.ProductNameRus.Contains(keyword)
                                                     || keyword == null)
                                                    .ToList(); // filter by keyword
            return View(products);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}