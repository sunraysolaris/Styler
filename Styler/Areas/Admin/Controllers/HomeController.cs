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
        //[HttpPost]
        public PartialViewResult ListProducts(SearchViewModel searchViewModel)
        {
            var model = new List<ProductsViewModel>();
            var products = DbContext
                .Products
                .Where(product => product.ProductName.Contains(searchViewModel.KeyWord)|| searchViewModel.KeyWord==null)
                .Select(product => new ProductsViewModel { ProductName = product.ProductName, Description = product.Description, Price = product.Price }).ToList();

            return PartialView("_Products", products);
        }

        public ActionResult Users()
        {
            var users = DbContext.Users;
            //  Mapp to Model
            return View(users);
        }
    }
}