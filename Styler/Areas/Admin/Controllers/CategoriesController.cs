using Styler.Areas.Admin.Models.Categories;
using Styler.Models;
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
        public ActionResult CategoriesList(int pageNumber = 1, string keyWord = null)
        {
            return View();
        }
        public PartialViewResult ListCategories(string keyword, int pagenumber = 1)
        {
            int itemPerPage = 10;
            var categoriesQuery = DbContext.Categories.Where(c => c.CategoryName.Contains(keyword)
                                                        || c.CategoryNameEng.Contains(keyword)
                                                        || c.CategoryNameRus.Contains(keyword) || keyword == null)
                                                        .Select(c => new CategoriesViewModel
                                                        {
                                                            CategoryID = c.CategoryID,
                                                            CategoryName = c.CategoryName,
                                                            CategoryNameEng = c.CategoryNameEng,
                                                            CategoryNameRus = c.CategoryNameRus
                                                        }).OrderBy(c => c.CategoryID);
            var pageCount = (int)Math.Ceiling((double)categoriesQuery.Count() / itemPerPage);

            var categories = categoriesQuery.Skip((pagenumber - 1) * itemPerPage).Take(itemPerPage).ToList();

            var model = new CategoriesListViewModel
            {
                CurrentPageNumber = pagenumber,
                PageCount = pageCount,
                KeyWord = keyword,
                Categories = categories,

            };
            return PartialView("_CategoriesGrid", model);
        }
        public ActionResult Edit(int? Id)
        {
            if (Id > 0)
            {
                var categoryToEdit = DbContext.Categories.FirstOrDefault(c => c.CategoryID == Id);
                if (categoryToEdit == null)
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
            int affectedRowCount = 0;
            if (ModelState.IsValid)
            {
                if (category.CategoryID > 0)
                {
                    var categoryToEdit = DbContext.Categories.FirstOrDefault(c => c.CategoryID == category.CategoryID);
                    if (categoryToEdit != null)
                    {
                        categoryToEdit.CategoryName = category.CategoryName;
                        categoryToEdit.CategoryNameEng = category.CategoryNameEng;
                        categoryToEdit.CategoryNameRus = category.CategoryNameRus;
                        affectedRowCount = DbContext.SaveChanges();
                    }
                    else
                    {
                        category.IsError = true;
                        category.Message = "მოხდა შეცდომა! მონაცემი არ მოიძებნა ბაზაში";
                        return View(category);
                    }
                }
                else
                {
                    DbContext.Categories.Add(new ProductCategory
                    {
                        CategoryName = category.CategoryName,
                        CategoryNameEng = category.CategoryNameEng,
                        CategoryNameRus = category.CategoryNameRus
                    });
                    affectedRowCount = DbContext.SaveChanges();
                }
                if (affectedRowCount > 0)
                {
                    category.IsError = false;
                    category.Message = "ცვლილება წარმატებით შეინახა";
                }
                else
                {
                    category.IsError = true;
                    category.Message = "მონაცემი არ შეიცვალა";
                }
            }
            return View(category);
        }
        public JsonResult Delete(int Id)
        {
            var categoryToDelete = DbContext.Categories.SingleOrDefault(p => p.CategoryID == Id);
            var success = false;
            string responseText = "მონაცემის წაშლისას მოხდა შცდომა";
            if (categoryToDelete != null)
            {
                DbContext.Categories.Remove(categoryToDelete);
                var status = DbContext.SaveChanges();
                if (status > 0)
                {
                    responseText = "მონაცემი წარმატებით წაიშალა";
                    success = true;
                }
            }
            return Json(new { message = responseText, success = success });
        }
    }
}