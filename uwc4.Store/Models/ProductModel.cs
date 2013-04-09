using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace uwc4.Store.Models
{
	public class ProductModel
	{
		public int ID { get; set; }
		public string Vendor { get; set; }
		public string Title { get; set; }
		public string Param { get; set; }
		public int? Price { get; set; }
		public string Description { get; set; }
	}
}