using Styler.Areas.Admin.Models.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Styler.Areas.Admin.Controllers
{
    public class CategoriesController : BaseController
    {
        // GET: Admin/Categories
        public ActionResult CategoriesList()
        {
            return View();
        }
        public PartialViewResult ListCategories(string keyword, int? pagenumber = 1)
        {
            var categories = DbContext.Categories.Where(c => c.CategoryName.Contains(keyword)
                                                        || c.CategoryNameEng.Contains(keyword)
                                                        || c.CategoryNameRus.Contains(keyword))
                                                        .Select(c=> new CategoriesViewModel
                                                        {
                                                            CategoryID = c.CategoryID,
                                                            CategoryName = c.CategoryName,
                                                            CategoryNameEng = c.CategoryNameEng,
                                                            CategoryNameRus =c.CategoryNameRus
                                                        }).ToList();
            var pageCount = (int)Math.Ceiling((double)categories.Count() / 10);

            var model = new CategoriesListViewModel
            {
                CurrentPageNumber = pagenumber.Value,
                PageCount = pageCount,
                KeyWord = keyword,
                Categories = categories,

            };
            return PartialView("_CategoriesGrid",model);
        }
        public ActionResult Edit(int? Id)
        {
            if (Id > 0)
            {
                var categoryToEdit = DbContext.Categories.FirstOrDefault(c => c.CategoryID == Id);
                if (categoryToEdit==null)
                {
                    return View("Error");
                }
                var model = new CategoriesViewModel
                {
                    CategoryID = categoryToEdit.CategoryID,
                    CategoryName = categoryToEdit.CategoryName,
                    CategoryNameEng = categoryToEdit.CategoryNameEng,
                    CategoryNameRus = categoryToEdit.CategoryNameRus
                };
                return View(model);
            }
            return View(new CategoriesViewModel());
        }
        [HttpPost]
        public ActionResult Edit(CategoriesViewModel category)
        {
            if (ModelState.IsValid)
            {
                // TODO !!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            }
            return View();
        }
    }
}