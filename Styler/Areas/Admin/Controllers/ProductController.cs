﻿using Styler.Areas.Admin.Models;
using Styler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Styler.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        public ActionResult ProductList()
        {
            return View();
        }
        //[HttpPost]
        public PartialViewResult ListProducts(SearchViewModel searchViewModel, int pageNumber = 1)
        {
            var ItemPerPage = 30; // პროდუქტი 1 გვერდზე
            var products = DbContext
                .Products
                .Where(product => product.ProductName.Contains(searchViewModel.KeyWord) || searchViewModel.KeyWord == null)
                .Select(product => new ProductViewModel { ProductID = product.ProductID, ProductName = product.ProductName, Description = product.Description, Price = product.Price })
                .OrderBy(product => product.ProductName)
                .Skip((pageNumber - 1) * ItemPerPage).ToList();

            var PageCount = (products.Count / ItemPerPage) - (products.Count % ItemPerPage);
            var model = new ProductsListViewModel
            {
                Products = products,
                PageCount = PageCount > 1 ? PageCount : 1,
                CurrentPageNumber = pageNumber
            };

            return PartialView("_ProductGrid", model);
        }
        public ActionResult Edit(int? Id)
        {
            if (Id > 0)
            {
                var ProductToEdit = DbContext.Products.SingleOrDefault(p => p.ProductID == Id);
                if (ProductToEdit == null)
                {
                    return View("Error");
                }

                var model = new ProductViewModel
                {
                    ProductID = ProductToEdit.ProductID,
                    ProductName = ProductToEdit.ProductName,
                    ProductNameEng = ProductToEdit.ProductNameEng,
                    ProductNameRus = ProductToEdit.ProductNameRus,
                    Description = ProductToEdit.Description,
                    DescriptionEng = ProductToEdit.DescriptionEng,
                    DescriptionRus = ProductToEdit.DescriptionRus,
                    Price = ProductToEdit.Price,
                    PhotoUrl = ProductToEdit.PhotoUrl
                };
                return View(model);
            }
            return View(new ProductViewModel());

        }
        [HttpPost]
        public ActionResult Edit(ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                int affectedRowCount = 0;
                if (product.ProductID > 0)// while Editing existed item
                {
                    var ProductToEdit = DbContext.Products.SingleOrDefault(p => p.ProductID == product.ProductID);
                    if (ProductToEdit != null)
                    {
                        ProductToEdit.ProductName = product.ProductName;
                        ProductToEdit.ProductNameEng = product.ProductNameEng;
                        ProductToEdit.ProductNameRus = product.ProductNameRus;
                        ProductToEdit.Description = product.Description;
                        ProductToEdit.DescriptionEng = product.DescriptionEng;
                        ProductToEdit.DescriptionRus = product.DescriptionRus;
                        ProductToEdit.Price = product.Price;
                        ProductToEdit.PhotoUrl = product.PhotoUrl;

                        affectedRowCount = DbContext.SaveChanges();
                    }
                    else
                    {
                        product.IsError = true;
                        product.Message = "Error Ocured!";
                    }
                }
                else // while adding new item
                {
                    DbContext.Products.Add(new Product
                    {
                        ProductName = product.ProductName,
                        ProductNameEng = product.ProductNameEng,
                        ProductNameRus = product.ProductNameRus,
                        Description = product.Description,
                        DescriptionEng = product.DescriptionEng,
                        DescriptionRus = product.DescriptionRus,
                        Price = product.Price,
                        PhotoUrl = product.PhotoUrl,
                    });
                    affectedRowCount = DbContext.SaveChanges();
                }
                if (affectedRowCount > 0) // if more then 0 row changed go to List
                {
                    return RedirectToAction("ProductList");
                }
            }

            return View(product);

        }

        public ActionResult Delete(int Id)
        {
            var ProductToDelete = DbContext.Products.SingleOrDefault(p => p.ProductID == Id);
            string responseText = "მონაცემის წაშლისას მოხდა შცდომა";
            if (ProductToDelete != null)
            {
                DbContext.Products.Remove(ProductToDelete);
                var status = DbContext.SaveChanges();
                if (status > 0)
                {
                    responseText = "მონაცემი წარმატებით წაიშალა";
                }
            }
            return Json(new { message = responseText });
            //return View("ProductList", new BaseViewModel { Message = result, IsError = true });
        }
    }
}