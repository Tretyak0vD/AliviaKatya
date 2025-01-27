using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Фотосалон.Classes
{
	public class Clients
    {
		public int Id { get; set; }
		public string FIO { get; set; }
		public string Email { get; set; }
        public string Password { get; set; }
        public Clients(int id, string fIO, string email, string password)
		{
			this.Id = id;
			this.FIO = fIO;
			this.Email = email;
			this.Password = password;
		}
		public static List<Clients> AllClients()
		{
			List<Clients> allClients = new List<Clients>();

			using (SqlConnection connection = Connection.Open())
			{
				SqlDataReader reader = Connection.Query("SELECT * FROM [dbo].[Clients]", connection);
				while (reader.Read())
				{
					allClients.Add(new Clients(
						reader.GetInt32(0),
						reader.GetString(1),
						reader.GetString(2),
						reader.GetString(3)
					));
				}
			}
			return allClients;
		}
		public static Clients GetClientByFIOAndEmail(string email, string password)
		{
			using (SqlConnection connection = Connection.Open())
			{
				string queryString = "SELECT * FROM [dbo].[Clients] WHERE EmailAddress = @EmailAddress AND Password = @Password";
				using (SqlCommand command = new SqlCommand(queryString, connection))
				{
					command.Parameters.AddWithValue("@EmailAddress", email);
                    command.Parameters.AddWithValue("@Password", password);

                    SqlDataReader reader = command.ExecuteReader();
					if (reader.Read())
					{
						return new Clients(
							reader.GetInt32(0),
							reader.GetString(1),
							reader.GetString(2),
							reader.GetString(3)
                        );
					}
				}
			}
			return null;
		}
	}
}
