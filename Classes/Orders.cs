using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Фотосалон.Classes
{
    public class Orders
    {
        public int Id { get; set; }
        public byte[] Image { get; set; }
        public int NumberCopies { get; set; }
        public DateTime OrderDate { get; set; }
        public int IdClient { get; set; }
        public string FIOEmployee { get; set; }
        public string MaterialName { get; set; }
        public string EquipmentName { get; set; }
        public string AddressSalon { get; set; }
        public decimal FullCost { get; set; }

        public Orders(int id, byte[] image, int numberCopies, DateTime orderDate, int idClient, string fIOEmployee, 
            string materialName, string equipmentName, string addressSalon, decimal fullCost) 
        { 
            this.Id = id;
            this.Image = image;
            this.NumberCopies = numberCopies;
            this.OrderDate = orderDate;
            this.IdClient = idClient;
            this.FIOEmployee = fIOEmployee;
            this.MaterialName = materialName;
            this.EquipmentName = equipmentName;
            this.AddressSalon = addressSalon;
            this.FullCost = fullCost;
        }
        public static List<Orders> AllOrders()
        {
            List<Orders> allOrders = new List<Orders>();

            using (SqlConnection connection = Connection.Open())
            {
                SqlDataReader reader = Connection.Query("SELECT * FROM [dbo].[Orders]", connection);
                while (reader.Read())
                {
                    allOrders.Add(new Orders(
                        reader.GetInt32(0),
                        reader.IsDBNull(1) ? null : (byte[])reader[1],
                        reader.GetInt32(2),
                        (DateTime)(reader.IsDBNull(3) ? (DateTime?)null : reader.GetDateTime(3)),
                        reader.GetInt32(4),
                        reader.GetString(5),
                        reader.GetString(6),
                        reader.GetString(7),
                        reader.GetString(8),
                        reader.GetDecimal(9))
                    );
                }
            }
            return allOrders;
        }
        public static List<Orders> GetOrdersByClient(int idClient)
        {
            List<Orders> clientsOrders = new List<Orders>();

            using (SqlConnection connection = Connection.Open())
            {
                SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[Orders] WHERE IdClient = @IdClient", connection);
                command.Parameters.AddWithValue("@IdClient", idClient);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    clientsOrders.Add(new Orders(
                        reader.GetInt32(0),
                        reader.IsDBNull(1) ? null : (byte[])reader[1],
                        reader.GetInt32(2),
                        (DateTime)(reader.IsDBNull(3) ? (DateTime?)null : reader.GetDateTime(3)),
                        reader.GetInt32(4),
                        reader.GetString(5),
                        reader.GetString(6),
                        reader.GetString(7),
                        reader.GetString(8),
                        reader.GetDecimal(9))
                    );
                }
            }
            return clientsOrders;
        }
    }
}
