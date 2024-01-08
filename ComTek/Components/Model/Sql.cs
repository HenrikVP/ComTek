using Microsoft.Data.SqlClient;

namespace ComTek.Components.Model
{
    public class Sql
    {
        // Connection string to our SQL server with the initial catalog set to our database. The login is Windows authentication (your user).
        private static string connectionString = "Data Source=HPOTEC\\Sqlexpress;Initial Catalog=WeatherForecastDB;Integrated Security=True;Trust Server Certificate=True;";

        // We connect to the database with SQL and return a list of weatherforecast objects.
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
                    }
                }
            }
            return wfList;
        }
    }
}
