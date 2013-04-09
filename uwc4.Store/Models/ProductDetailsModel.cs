using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using uwc4.Store.Helpers;

namespace uwc4.Store.Models
{
	public class ProductDetailsModel
	{
		public ProductModel Product { get; set; }
		public Dictionary<string, string> Properties { get; set; }

		public ProductDetailsModel(string product)
		{
			using (var c = DBInterface.Command("GetProductInfo"))
			{
				c.Parameters.AddWithValue("@product", product);

				using(var r = c.ExecuteReader())
				{
					//общая информация о товаре
					if (r.Read())
					{
						var p = new ProductModel();
						p.ID = Convert.ToInt32(r["ID"]);
						p.Vendor = r["Vendor"] as string;
						p.Title = r["Title"] as string;
						p.Param = r["Param"] as string;
						p.Price = (int?)r["Price"];
						p.Description = r["Description"] as string;
						this.Product = p;
					}

					//характеристики товара
					if (r.NextResult())
					{
						this.Properties = new Dictionary<string, string>();
						while (r.Read())
						{
							this.Properties.Add(r["Title"] as string, r["Value"] as string);
						}
					}
				}
			}
		}
	}
}