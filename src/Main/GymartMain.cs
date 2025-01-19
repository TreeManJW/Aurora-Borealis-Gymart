using LightPolution;
using Weather;


// Ask user for latitude, parese it and check if it is valid
var latitude = 5.5;
// Ask user for longitude, parese it and check if it is valid
var longitude = 2.2;

var lightPolutionCalculator = new LightPolutionCalculator();
var lightPolution = lightPolutionCalculator.GetLightPolution(latitude, longitude);

var weatherCalculator = new WeatherCalculator();
var weather = weatherCalculator.GetCurrentWeather(latitude, longitude);

// Call Nasa API to get KP index (int)

var time = DateTime.Now;

// Call custom library to get the chance of seeing the northern lights (double)

// Print the results