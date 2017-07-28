using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Styler.Areas.Admin.Models
{
    public class BaseViewModel
    {
        public string Message { get; set; }
        public bool IsError { get; set; }
    }
}