using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Фотосалон.Classes
{
    public class PrintShops
    {
        public int Id { get; set; }
        public string AddressSalon { get; set; }
        public string OpeningHours { get; set; }

        public PrintShops(int id, string addressSalon, string openingHours) 
        {
            this.Id = id;
            this.AddressSalon = addressSalon;
            this.OpeningHours = openingHours;
        }
        public static List<PrintShops> AllPrintShops()
        {
            List<PrintShops> allPrintShops = new List<PrintShops>();

            using (SqlConnection connection = Connection.Open())
            {
                SqlDataReader reader = Connection.Query("SELECT * FROM [dbo].[PrintShops]", connection);
                while (reader.Read())
                {
                    allPrintShops.Add(new PrintShops(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2)
                    ));
                }
            }
            return allPrintShops;
        }
    }
}
