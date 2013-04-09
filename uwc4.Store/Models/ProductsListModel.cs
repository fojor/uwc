using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using uwc4.Store.Helpers;

namespace uwc4.Store.Models
{
	public class ProductsListModel
	{
		public string Title { get; set; }
		public List<ProductModel> Items { get; set; }

		public ProductsListModel(string category, string filter)
		{
			this.Items = new List<ProductModel>();

			using (var c = DBInterface.Command("GetProducts"))
			{
				//параметр категории товаров
				c.Parameters.AddWithValue("@Category", category);

				//табличный параметр с выбраными опциями фильтра
				c.Parameters.Add(Utils.SerializeToFilterParam(filter));

				using (var r = c.ExecuteReader())
				{
					//заголовок категории товаров
					if (r.Read())
						this.Title = r["Title"] as string;

					//список товаров
					if (r.NextResult())
					{
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
}