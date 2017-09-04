using Styler.Areas.Admin.Models;
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
        int ItemPerPage = 10; // პროდუქტი 1 გვერდზე
        public ActionResult ProductList(int? pageNumber = 1, string Keyword = null)
        {
            return View();
        }
        //[HttpPost]
        public PartialViewResult ListProducts(string keyWord, int pageNumber = 1)
        {

            var productsQuery = DbContext
                .Products
                .Where(product => product.ProductName.Contains(keyWord) || keyWord == null)
                .Select(product => new ProductViewModel
                {
                    ProductID = product.ProductID,
                    ProductName = product.ProductName,
                    Description = product.Description,
                    Price = product.Price,
                    CurrentCategories = product.ProductCategories
                })
                .OrderBy(product => product.ProductID);

            var pageCount = (int)Math.Ceiling((double)productsQuery.Count() / ItemPerPage);

            var products = productsQuery
                .Skip((pageNumber - 1) * ItemPerPage)
                .Take(ItemPerPage).ToList();

            var model = new ProductsListViewModel
            {
                KeyWord = keyWord,
                PageCount = pageCount,
                Products = products,
                CurrentPageNumber = pageNumber
            };

            return PartialView("_ProductGrid", model);
        }
        public ActionResult Edit(int? Id)
        {
            var categoryList = DbContext.Categories.ToList();
            if (Id > 0)
            {
                var ProductToEdit = DbContext.Products.Include("ProductCategories").SingleOrDefault(p => p.ProductID == Id);

                if (ProductToEdit == null)
                {
                    return View("Error");
                }

                var model = new ProductEditViewModel
                {
                    ProductID = ProductToEdit.ProductID,
                    ProductName = ProductToEdit.ProductName,
                    ProductNameEng = ProductToEdit.ProductNameEng,
                    ProductNameRus = ProductToEdit.ProductNameRus,
                    Description = ProductToEdit.Description,
                    DescriptionEng = ProductToEdit.DescriptionEng,
                    DescriptionRus = ProductToEdit.DescriptionRus,
                    Price = ProductToEdit.Price,
                    PhotoUrl = ProductToEdit.PhotoUrl,
                    CurrentCategories = ProductToEdit.ProductCategories.Select(p => p.CategoryID).ToArray(),
                    CategoryList = categoryList
                };
                return View(model);
            }
            return View(new ProductEditViewModel() { CategoryList = categoryList });

        }
        [HttpPost]
        public ActionResult Edit(ProductEditViewModel product)
        {
            if (ModelState.IsValid)
            {
                int affectedRowCount = 0;
                if (product.ProductID > 0)// while Editing existing item
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
                        DbContext.Entry(ProductToEdit).Collection("ProductCategories").Load();
                        ProductToEdit.ProductCategories.Clear();
                        ProductToEdit.ProductCategories = DbContext.Categories.Where(i => product.CurrentCategories.Contains(i.CategoryID)).ToList();
                        affectedRowCount = DbContext.SaveChanges();

                    }
                    else
                    {
                        product.IsError = true;
                        product.Message = "მოხდა შეცდომა! მონაცემი არ მოიძებნა ბაზაში";
                        return View(product);
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
                        ProductCategories = DbContext.Categories.Where(pc => product.CurrentCategories.Contains(pc.CategoryID)).ToList()
                    });
                    affectedRowCount = DbContext.SaveChanges();
                }
                if (affectedRowCount > 0)
                {
                    product.IsError = false;
                    product.Message = "ცვლილება წარმატებით შეინახა";
                }
                else
                {
                    product.IsError = true;
                    product.Message = "მონაცემი არ შეიცვალა";
                }
            }

            return View(product);

        }

        public JsonResult Delete(int Id)
        {
            var ProductToDelete = DbContext.Products.SingleOrDefault(p => p.ProductID == Id);
            string responseText = "მონაცემის წაშლისას მოხდა შცდომა";
            var success = false;
            if (ProductToDelete != null)
            {
                DbContext.Products.Remove(ProductToDelete);
                var status = DbContext.SaveChanges();
                if (status > 0)
                {
                    responseText = "მონაცემი წარმატებით წაიშალა";
                    success = true;
                }
            }
            return Json(new { message = responseText, success = success });
            //return View("ProductList", new BaseViewModel { Message = result, IsError = true }); 
            //BaseViewModel საჭირო იყო სტატუსის შეტყობინების გადასაცემად. ეხლა ეს ხდება Toster Jquery plugin-ით
        }
    }
}