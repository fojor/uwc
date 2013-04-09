using System.Web;
using System.Web.Optimization;

namespace uwc4.Store
{
	public class BundleConfig
	{
		public static void RegisterBundles(BundleCollection bundles)
		{
			var s = new ScriptBundle("~/js");
			s.Include("~/Scripts/bootstrap.js");
			s.Include("~/Scripts/general.js");
			bundles.Add(s);

			var c = new StyleBundle("~/css");
			c.Include("~/Content/css/bootstrap.css");
			c.Include("~/Content/css/bootstrap-responsive.css");
			c.Include("~/Content/css/site.css");
			bundles.Add(c);
		}
	}
}