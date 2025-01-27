using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Фотосалон.Classes
{
	public class Employee
	{
		public int Id { get; set; }
		public string FIO { get; set; }
        public string Email { get; set; }
        public string Post { get; set; }

		public Employee(int id, string fIO, string email, string post)
		{
			this.Id = id;
			this.FIO = fIO;
			this.Email = email;
			this.Post = post;
		}
		public static List<Employee> AllEmployee()
		{
			List<Employee> allEmployee = new List<Employee>();

			using (SqlConnection connection = Connection.Open())
			{
				SqlDataReader reader = Connection.Query("SELECT * FROM [dbo].[Employee]", connection);
				while (reader.Read())
				{
					allEmployee.Add(new Employee(
						reader.GetInt32(0),
						reader.GetString(1),
						reader.GetString(2),
						reader.GetString(3)
					));
				}
			}
			return allEmployee;
		}
	}
}