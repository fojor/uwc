using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using uwc4.Store.App_Start;

namespace uwc4.Store
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				name: "Search",
				url: "search",
				defaults: new { controller = "Products", action = "Search" }
			);

			routes.MapRoute(
				name: "Products",
				url: "{category}",
				defaults: new { controller = "Products", action = "List" },
				constraints: new { c = new CategoriesConstraint() }
			);

			routes.MapRoute(
				name: "Product",
				url: "{category}/{product}",
				defaults: new { controller = "Products", action = "Details" },
				constraints: new { c = new CategoriesConstraint() }
			);

			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}