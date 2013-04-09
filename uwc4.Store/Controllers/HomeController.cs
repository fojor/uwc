using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using uwc4.Store.Helpers;
using uwc4.Store.Models;

namespace uwc4.Store.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
			return RedirectToAction("List", "Products", new { category = "planshety" });
        }

		public ActionResult Header()
		{
			var model = new HeaderModel();

			return PartialView(model);
		}
	}
}
