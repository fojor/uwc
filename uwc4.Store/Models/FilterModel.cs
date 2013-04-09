using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using uwc4.Store.Helpers;

namespace uwc4.Store.Models
{
	public class FilterGroup
	{
		public string Title { get; set; }
		public bool HasCheckedItems { get; set; }
		public List<FilterItem> Items { get; set; }
	}

	public class FilterItem
	{
		public int PropertyID { get; set; }
		public int ValueID { get; set; }
		public string Title { get; set; }
		public string Link { get; set; }
		public int Count { get; set; }
		public bool Checked { get; set; }
		public bool Disabled { get; set; }
	}

	public class FilterModel
	{
		public List<FilterGroup> Items { get; set; }

		public FilterModel(string category, string filter)
		{
			var tmpgroups = new Dictionary<int, FilterGroup>();

			using (var c = DBInterface.Command("GetFilterProperties"))
			{
				//параметр категории товаров
				c.Parameters.AddWithValue("@Category", category);

				//табличный параметр с выбраными опциями фильтра
				c.Parameters.Add(Utils.SerializeToFilterParam(filter));

				using (var r = c.ExecuteReader())
				{
					//список групп фильтра
					while (r.Read())
					{
						var group = new FilterGroup();
						group.Title = r["Title"] as string;
						group.Items = new List<FilterItem>();
						tmpgroups.Add(Convert.ToInt32(r["ID"]), group);
					}

					//список опций фильтра
					if (r.NextResult())
					{
						while (r.Read())
						{
							var item = new FilterItem();
							item.Title = r["Title"] as string;
							item.PropertyID = Convert.ToInt32(r["PropertyID"]);
							item.ValueID = Convert.ToInt32(r["ValueID"]);
							item.Count = Convert.ToInt32(r["Count"]);
							if (r["Checked"] != DBNull.Value)
								item.Checked = true;
							tmpgroups[item.PropertyID].Items.Add(item);
						}
					}
				}
			}

			//записываем с временного словаря в модель, 
			//устанавливаем дополнительные свойства для опций фильтра
			this.Items = new List<FilterGroup>();
			foreach (var tmpgroup in tmpgroups.Values)
			{
				var group = new FilterGroup();
				group.Title = tmpgroup.Title;
				group.Items = new List<FilterItem>();

				var link = filter;
				if(!string.IsNullOrEmpty(link))
					foreach (var item in tmpgroup.Items.Where(i => i.Count == 0))
						link = link.Replace(string.Format("{0}={1};", item.PropertyID, item.ValueID), "");

				foreach (var item in tmpgroup.Items)
				{
					if (item.Count > 0)
					{
						if (item.Checked || item.Count == 0)
							item.Link = link.Replace(string.Format("{0}={1};", item.PropertyID, item.ValueID), "");
						else
							item.Link = string.Format("{0}{1}={2};", link, item.PropertyID, item.ValueID);
					}
					else
					{
						item.Disabled = true;
					}

					if (item.Checked && !group.HasCheckedItems)
						group.HasCheckedItems = true;

					group.Items.Add(item);
				}

				this.Items.Add(group);
			}
		}
	}
}