using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Фотосалон.Classes
{
    public class Equipment
    {
        public int Id { get; set; }
        public string EquipmentName { get; set; }
        public decimal CostPerUnit { get; set; }
        public Equipment(int id, string equipmentName, decimal costPerUnit)
        { 
            this.Id = id;
            this.EquipmentName = equipmentName;
            this.CostPerUnit = costPerUnit;
        }
        public static List<Equipment> AllEquipment()
        {
            List<Equipment> allEquipment = new List<Equipment>();

            using (SqlConnection connection = Connection.Open())
            {
                SqlDataReader reader = Connection.Query("SELECT * FROM [dbo].[Equipment]", connection);
                while (reader.Read())
                {
                    allEquipment.Add(new Equipment(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetDecimal(2)
                    ));
                }
            }
            return allEquipment;
        }
    }
}
