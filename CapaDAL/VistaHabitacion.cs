using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDAL
{
//    public class VistaHabitacion
//    {
//        private string connectionString;

//        public VistaHabitacion(string connectionString)
//        {
//            this.connectionString = connectionString;
//        }

//        public void MostrarVistaHabitacion()
//        {
//            string query = "SELECT TipoDeHabitacion, NombreHabitacion, Estado FROM VistaHabitacion";

//            using (SqlConnection connection = new SqlConnection(connectionString))
//            {
//                connection.Open();

//                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
//                {
//                    DataSet dataSet = new DataSet();
//                    adapter.Fill(dataSet);

//                    foreach (DataRow row in dataSet.Tables[0].Rows)
//                    {
//                        string tipoHabitacion = row["TipoDeHabitacion"].ToString();
//                        string NombreHabitacion = row["NombreHabitacion"].ToString();
//                        string state = row["Estado"].ToString();

//                        Console.WriteLine($"Tipo de Habitación: {tipoHabitacion}, Nombre de Habitación: {NombreHabitacion}, Estado: {state}");
//                    }
//                }
//            }
//    }   }
}
