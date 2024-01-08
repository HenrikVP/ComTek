using Microsoft.Data.SqlClient;

namespace ComTek.Components.Model
{
    public class Sql
    {
        private static string connectionString = "Data Source=HPOTEC\\Sqlexpress;Initial Catalog=WeatherForecastDB;Integrated Security=True;Trust Server Certificate=True;";

        public static void Select(string queryString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(queryString, connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(String.Format("{0}", reader[2]));
                    }
                }
            }
        }
    }
}
