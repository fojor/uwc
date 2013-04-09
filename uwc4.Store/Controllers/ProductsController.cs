using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using uwc4.Store.Helpers;
using uwc4.Store.Models;

namespace uwc4.Store.Controllers
{
    public class ProductsController : Controller
    {
		public ActionResult List(string category, string filter)
        {
			var model = new ProductsListModel(category, filter);

			return View(model);
        }

		public ActionResult Filter(string category, string filter)
		{
			var model = new FilterModel(category, filter);

			return PartialView(model);
		}

		public ActionResult Product(ProductModel product)
		{
			return PartialView(product);
		}

		public ActionResult Details(string category, string product)
		{
			var model = new ProductDetailsModel(product);

			return View(model);
		}

		public ActionResult Search(string query)
		{
			var model = new SearchResultModel(query);

			return View(model);
		}

		[OutputCache(Duration = 60 * 60 * 3, VaryByParam = "*")]
		public FileContentResult Image(int id)
		{
			using (var c = DBInterface.Command("GetProductImage"))
			{
				c.Parameters.AddWithValue("@id", id);
				using (SqlDataReader reader = c.ExecuteReader())
					if (reader.Read())
					{
						byte[] data = reader["data"] as byte[] ?? null;
						if (data != null)
							return File(data, reader["mimetype"] as string);
					}
			}

			throw new HttpException(404, "Not found");
		}

		public ActionResult Upload()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Upload(int id, HttpPostedFileBase image)
		{
			byte[] buffer = new byte[image.ContentLength];
			Stream s = image.InputStream;
			s.Read(buffer, 0, buffer.Length);

			var query = "insert into images (productid, mimetype, data) values (@id, @type, @data)";
			using (var c = DBInterface.Command(query, false))
			{
				c.Parameters.AddWithValue("@id", id);
				c.Parameters.AddWithValue("@type", image.ContentType);
				c.Parameters.AddWithValue("@data", buffer);
				c.ExecuteNonQuery();
			}

			return View();
		}
    }
}
