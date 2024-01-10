using Microsoft.Data.SqlClient;
using System.Data;

namespace ComTek.Components.Model
{
    public class Sql
    {

        // WARNING! NEVER put your connectionstring into the code that will be pushed to GITHUB
        // Connection string to our SQL server with the initial catalog set to our database. The login is Windows authentication (your user).
//        private static string connectionString = "Data Source=HPOTEC\\Sqlexpress;Initial Catalog=WeatherForecastDB;Integrated Security=True;Trust Server Certificate=True; Connect Timeout=3";
        private static string connectionString = "Data Source=.,10433;Initial Catalog=WeatherForecastDB;User ID=sa;Password=Passw0rd;Connect Timeout=3;Encrypt=True;Trust Server Certificate=True;";

        // We connect to the database with SQL and return a list of weatherforecast objects.
        //public static async Task<List<WeatherForecast>> GetWeatherForecastsAsync(string queryString)
        public static async Task<List<T>> GetWeatherForecastsAsync<T>(string queryString)
        {
            List<T> wfList = new();

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
                        wf.Id = (int)reader[3];
                        //wfList.Add(wf);
                        wfList.Add((T)(object)wf);// Convert.ChangeType(wf, typeof(T)));
                    }
                }
            }
            return wfList;
        }

        /// <summary>
        /// Multi-use method that can be used with Insert/Update/Delete
        /// </summary>
        /// <param name="wf">Weatherforecast object</param>
        /// <param name="queryString">The SQL query</param>
        public static void NonQuery(WeatherForecast wf, string queryString)
        {
            using (SqlConnection connection = new(connectionString))
            {
                SqlCommand command = new(queryString, connection);
                command.Parameters.Add("@id", SqlDbType.Int).Value = wf.Id;
                command.Parameters.Add("@date", SqlDbType.Date).Value = wf.Date;
                command.Parameters.Add("@tempC", SqlDbType.Int).Value = wf.TemperatureC;
                command.Parameters.Add("@summary", SqlDbType.NVarChar).Value = wf.Summary;

                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}