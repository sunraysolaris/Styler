using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Styler.Core;
namespace Styler.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        protected ApplicationDbContext DbContext;
        public BaseController()
        {
            DbContext = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            DbContext.Dispose();
            base.Dispose(disposing);
        }
    }
}