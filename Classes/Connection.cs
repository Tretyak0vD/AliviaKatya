using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Фотосалон.Classes
{
	public class Connection
	{
		public static SqlConnection Open()
		{
			string conn = $"Data Source=localhost;Initial Catalog=fotohut;Integrated Security=True";
			SqlConnection connection = new SqlConnection(conn);
			connection.Open();
			return connection;
		}

		public static void CloseConnection(SqlConnection connection)
		{
			connection.Close();
			SqlConnection.ClearPool(connection);
		}

		public static SqlDataReader Query(string queryString, SqlConnection connection)
		{
			SqlCommand command = new SqlCommand(queryString, connection);
			SqlDataReader reader = command.ExecuteReader();
			return reader;
		}
	}
}