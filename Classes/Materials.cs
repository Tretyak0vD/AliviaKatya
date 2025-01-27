using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Фотосалон.Classes
{
    public class Materials
    {
        public int Id { get; set; }
        public string MaterialName { get; set; }
        public int MaterialRest { get; set; }
        public decimal CostPerUnit { get; set; }

        public Materials(int id, string materialName, int materialRest, decimal costPerUnit) 
        {
            this.Id = id;
            this.MaterialName = materialName;
            this.MaterialRest = materialRest;
            this.CostPerUnit = costPerUnit;
        }
        public static List<Materials> AllMaterials()
        {
            List<Materials> allMaterials = new List<Materials>();

            using (SqlConnection connection = Connection.Open())
            {
                SqlDataReader reader = Connection.Query("SELECT * FROM [dbo].[Materials]", connection);
                while (reader.Read())
                {
                    allMaterials.Add(new Materials(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetInt32(2),
                        reader.GetDecimal(3)
                    ));
                }
            }
            return allMaterials;
        }
    }
}
