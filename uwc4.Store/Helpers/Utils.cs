using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace uwc4.Store.Helpers
{
	public class Utils
	{
		/// <summary>
		/// сериализует get-параметр фильтра в табличный параметр
		/// </summary>
		public static SqlParameter SerializeToFilterParam(string filter)
		{
			var dt = new DataTable();
			dt.Columns.Add("id");
			dt.Columns.Add("value");

			if (!string.IsNullOrEmpty(filter))
			{
				var pairs = filter.Split(';');
				foreach (var pair in pairs)
				{
					if (pair.IndexOf('=') > 0)
					{
						var v = pair.Split('=');
						var propID = v[0];
						var propVal = v[1].Replace(",", ".");

						if (!string.IsNullOrEmpty(propID) && !string.IsNullOrEmpty(propVal))
						{
							var row = dt.NewRow();
							row["id"] = propID;
							row["value"] = propVal;
							dt.Rows.Add(row);
						}
					}
				}
			}

			var param = new SqlParameter();
			param.SqlDbType = SqlDbType.Structured;
			param.ParameterName = "@Filter";
			param.TypeName = "dbo.FilterValues";
			param.SqlValue = dt;

			return param;
		}
	}
}