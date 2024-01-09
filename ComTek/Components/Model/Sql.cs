using Microsoft.Data.SqlClient;
using System.Data;

namespace ComTek.Components.Model
{
    public class Sql
    {
        // Connection string to our SQL server with the initial catalog set to our database. The login is Windows authentication (your user).
        private static string connectionString = "Data Source=HPOTEC\\Sqlexpress;Initial Catalog=WeatherForecastDB;Integrated Security=True;Trust Server Certificate=True;";

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
                        //wfList.Add(wf);
                        wfList.Add((T)(object)wf);// Convert.ChangeType(wf, typeof(T)));
                    }
                }
            }
            return wfList;
        }

        public static void CreateWeatherForecast(WeatherForecast wf)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("INSERT INTO Weatherforecast ([Date], TemperatureC, Summary) VALUES (@date, @tempC, @summary)", connection);
                command.Parameters.Add("@date", SqlDbType.Date).Value = wf.Date;
                command.Parameters.Add("@tempC", SqlDbType.Int).Value = wf.TemperatureC;
                command.Parameters.Add("@summary", SqlDbType.NVarChar).Value = wf.Summary;

                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public static void DeleteWeatherForecast(WeatherForecast wf)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("DELETE FROM Weatherforecast WHERE Date = @date AND TemperatureC = @tempC AND Summary = @summary", connection);
                command.Parameters.Add("@date", SqlDbType.Date).Value = wf.Date;
                command.Parameters.Add("@tempC", SqlDbType.Int).Value = wf.TemperatureC;
                command.Parameters.Add("@summary", SqlDbType.NVarChar).Value = wf.Summary;

                command.Connection.Open();
                command.ExecuteNonQuery();
            }

        }
    }
}