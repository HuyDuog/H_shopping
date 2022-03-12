using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HShopping.Controllers
{
    public class AjaxContentController : Controller
    {
        public IActionResult HearderCart()
        {
            return ViewComponent("HearderCart");
        }
        public IActionResult NumberCart()
        {
            return ViewComponent("NumberCart");
        }
    }
}
