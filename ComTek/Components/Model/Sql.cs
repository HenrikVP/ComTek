using Microsoft.Data.SqlClient;

namespace ComTek.Components.Model
{
    public class Sql
    {
        private static string connectionString = "Data Source=HPOTEC\\Sqlexpress;Initial Catalog=WeatherForecastDB;Integrated Security=True;Trust Server Certificate=True;";

        public static async Task<List<WeatherForecast>> GetWeatherForecastsAsync(string queryString)
        {
            List<WeatherForecast> wfList = new();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(queryString, connection);
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        WeatherForecast wf = new();
                        wf.Date = DateOnly.FromDateTime((DateTime)reader[0]);
                        wf.TemperatureC = (int)reader[1];
                        wf.Summary = (string)reader[2];
                        wfList.Add(wf);
                        //Console.WriteLine(String.Format("{0}", reader[2]));
                    }
                }
            }
            return wfList;
        }
    }
}
