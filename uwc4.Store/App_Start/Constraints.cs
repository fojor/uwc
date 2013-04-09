using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Routing;
using uwc4.Store.Helpers;

namespace uwc4.Store.App_Start
{
	public class CategoriesConstraint : IRouteConstraint
	{
		public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
		{
			var list = HttpContext.Current.Cache["Categories"] as List<string>;

			if (list == null)
			{
				list = new List<string>();

				using (var c = DBInterface.Command("GetCategories"))
				using (var r = c.ExecuteReader())
					while (r.Read())
						list.Add(r["Param"] as string);

				if (HttpContext.Current.Cache["Categories"] == null)
					HttpContext.Current.Cache.Insert("Categories", list, null, DateTime.Now.AddMinutes(30), Cache.NoSlidingExpiration);
			}

			return list.Contains(values["category"] as string);
		}
	}
}