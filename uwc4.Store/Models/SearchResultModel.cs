using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using uwc4.Store.Helpers;

namespace uwc4.Store.Models
{
	public class SearchResultModel
	{
		public List<ProductModel> Items { get; set; }

		public SearchResultModel(string query)
		{
			this.Items = new List<ProductModel>();

			using (var c = DBInterface.Command("SearchProducts"))
			{
				//параметр поиска
				c.Parameters.AddWithValue("@Query", query);

				using (var r = c.ExecuteReader())
				{
					//список найденных товаров
					while (r.Read())
					{
						var product = new ProductModel();
						product.ID = Convert.ToInt32(r["ID"]);
						product.Vendor = r["Vendor"] as string;
						product.Title = r["Title"] as string;
						product.Param = r["Param"] as string;
						product.Price = (int?)r["Price"];
						product.Description = r["Description"] as string;

						this.Items.Add(product);
					}
				}
			}
		}
	}
}