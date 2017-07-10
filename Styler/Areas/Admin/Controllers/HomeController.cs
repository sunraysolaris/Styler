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
            return View();
        }
        public ActionResult Users()
        {
            var users = DbContext.Users;
            //  Mapp to Model
            return View(users);
        }
    }
}