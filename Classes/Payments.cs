using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Фотосалон.Classes
{
    public class Payments
    {
        public int Id { get; set; }
        public int IdOrder { get; set; }
        public decimal FullCost { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentStatus { get; set; }
        public string PaymentMethod { get; set; }
        public int IdClient { get; set; }

        public Payments(int id, int idOrder, decimal fullCost, DateTime paymentDate, string paymentStatus, string paymentMethod, int idClient) 
        {
            this.Id = id;
            this.IdOrder = idOrder;
            this.FullCost = fullCost;
            this.PaymentDate = paymentDate;
            this.PaymentStatus = paymentStatus;
            this.PaymentMethod = paymentMethod;
            this.IdClient = idClient;
        }
        public static List<Payments> AllPayments()
        {
            List<Payments> allPayments = new List<Payments>();

            using (SqlConnection connection = Connection.Open())
            {
                SqlDataReader reader = Connection.Query("SELECT * FROM [dbo].[Payments]", connection);
                while (reader.Read())
                {
                    allPayments.Add(new Payments(
                        reader.GetInt32(0),
                        reader.GetInt32(1),
                        reader.GetDecimal(2),
                        (DateTime)(reader.IsDBNull(3) ? (DateTime?)null : reader.GetDateTime(3)),
                        reader.GetString(4),
                        reader.GetString(5),
                        reader.GetInt32(6))
                    );
                }
            }
            return allPayments;
        }
        public static List<Payments> GetPaymentsByClient(int idClient)
        {
            List<Payments> clientsPayments = new List<Payments>();

            using (SqlConnection connection = Connection.Open())
            {
                SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[Payments] WHERE IdClient = @IdClient", connection);
                command.Parameters.AddWithValue("@IdClient", idClient);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    clientsPayments.Add(new Payments(
                        reader.GetInt32(0),
                        reader.GetInt32(1),
                        reader.GetDecimal(2),
                        (DateTime)(reader.IsDBNull(3) ? (DateTime?)null : reader.GetDateTime(3)),
                        reader.GetString(4),
                        reader.GetString(5),
                        reader.GetInt32(6))
                    );
                }
            }
            return clientsPayments;
        }
    }
}
