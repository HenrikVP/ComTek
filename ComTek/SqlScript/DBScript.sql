USE master
GO
CREATE DATABASE WeatherForecastDB
GO
USE WeatherForecastDB
GO

CREATE TABLE WeatherForecast (
[Date] DATE,
TemperatureC INT,
Summary NVARCHAR(100)
)

INSERT INTO WeatherForecast ([Date], TemperatureC, Summary) VALUES ('2024-01-08', -7, 'Pissekoldt')

SELECT * FROM WeatherForecast