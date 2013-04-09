using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace uwc4.Store.Helpers
{
	public class DBInterface
	{
		public static string GetConnectionString()
		{
			return ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
		}
		
		public static SqlCommand Command(string text, bool sp = true)
		{
			var conn = new SqlConnection(GetConnectionString());
			conn.Open();
			
			var cmd = new SqlCommand(text, conn);

			if(sp)
				cmd.CommandType = CommandType.StoredProcedure;

			cmd.Disposed += cmd_Disposed;

			return cmd;
		}

		static void cmd_Disposed(object sender, EventArgs e)
		{
			var cmd = (SqlCommand)sender;
			cmd.Connection.Close();
		}
	}
}