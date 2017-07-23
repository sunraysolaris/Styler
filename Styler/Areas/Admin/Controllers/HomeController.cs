using Styler.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Styler.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductList()
        {
            var products = DbContext.Products.ToList();
            var model = new List<ProductsViewModel>();
            products.ForEach(i =>
            model.Add(new ProductsViewModel
            {
                ProductID = i.ProductID,
                ProductName = i.ProductName,
                ProductNameEng = i.ProductNameEng,
                ProductNameRus = i.ProductNameRus,
                Description = i.Description,
                DescriptionEng = i.DescriptionEng,
                DescriptionRus = i.DescriptionRus,
                Price = i.Price,
                PhotoUrl = i.PhotoUrl
            }));
            return View(model);
        }
        [HttpPost]
        public PartialViewResult ListEm(SearchViewModel searchViewModel)
        {
            var model = new List<ProductsViewModel>();
            model.Add(new ProductsViewModel { ProductName = "xx", Description = "yy", Price = 5 });
            model.Add(new ProductsViewModel { ProductName = "ff", Description = "yy", Price = 5 });
            model.Add(new ProductsViewModel { ProductName = "dd", Description = "yy", Price = 5 });
            model.Add(new ProductsViewModel { ProductName = "zz", Description = "yy", Price = 5 });
            var res = model.Where(i => i.ProductName.Contains(searchViewModel.KeyWord));
            return PartialView("_Products", res);
        }

        public ActionResult Users()
        {
            var users = DbContext.Users;
            //  Mapp to Model
            return View(users);
        }
    }
}