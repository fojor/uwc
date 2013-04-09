using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using uwc4.Store.Helpers;

namespace uwc4.Store.Models
{
	public class MenuItem
	{
		public string Title { get; set; }
		public string Param { get; set; }
	}

	public class HeaderModel
	{
		public List<MenuItem> MenuItems { get; set; }

		public HeaderModel()
		{
			this.MenuItems = new List<MenuItem>();

			using (var c = DBInterface.Command("GetCategories"))
			using (var r = c.ExecuteReader())
				while (r.Read())
					this.MenuItems.Add(new MenuItem { Title = r["Title"] as string, Param = r["Param"] as string });
		}
	}
}