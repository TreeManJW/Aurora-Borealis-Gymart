using System.Diagnostics.CodeAnalysis;
using LightPolution;
using Weather;


// This program starts with asking the user for coordinates in the decimal minute format. It then processes these coordinates in a way so that both the Weather.cs and LightPolution.cs program can run on these values.
// The program then calls the Nasa API to get the KP index and a custom library to get the chance of seeing the northern lights.

// DegreesAndNM are used to store parsed and split up longitude and latitude.
string[] degreesAndNM = ["PlaceHolder", "PlaceHolder1"];

// These are used to determine if the location is east of Greenwich and north of the equator. This is used to calculate the correct direction on the image as coordinate don't have their starting point where the image has it's pixel startpoint in the top left.
bool isEastOfGreenwich = true;
bool isNorthOfEquator = true;

// These are used to store the amount of pixels from left to right and top to bottom on the image.
int leftToRightAmoutnOfPixels = 0;
int topToBottomAmoutnOfPixels = 0;

// Initiate the variables for the latitude and longitude that are going to be in the decimal degrees format. so instead of 59N11 it will be 59.1833. This is used by the Weather.cs library as the API there demands it.
// Latitude in decimal degrees are also used to create a normalized value of the latitudal direction on the mercator projection.
double latitudeInDecimalDegrees = 0;
double longitudeInDecimalDegrees = 0;


// Ask user for latitude.
Console.WriteLine("What is the latitude? (ex 59N11 or 59S11 Note: Between 75N and 65S)");
var latitude = Console.ReadLine();

if (latitude == null)
{
    Console.WriteLine("You must enter a valid value");
    return;
}

// Checks the direction from the equator the coordinates go from. Then it splits the string into degrees and nautical miles.
if (latitude.Contains('N'))
{
    degreesAndNM = latitude.Split('N');
}
else if (latitude.Contains('S'))
{
    degreesAndNM = latitude.Split('S');
    isNorthOfEquator = false;
}
else
{
    Console.WriteLine("You must enter a valid value with the example 59N11 or 59S11 meaning 59°11' north and south respectively.");
    return;
}

// Parse the degrees and nautical miles.
bool isCorrectDegrees = int.TryParse(degreesAndNM[0], out int degrees);
bool isCorrectNM = int.TryParse(degreesAndNM[1], out int nauticalMiles);

// If the location is south of the equator the degrees are made negative.
if (!isNorthOfEquator)
{
    degrees = -degrees;
    nauticalMiles = -nauticalMiles;
}
if (isCorrectDegrees && isCorrectNM)
{
    // Calculate the latitude in decimal degrees.
    latitudeInDecimalDegrees = (double)degrees + ((double)nauticalMiles / 60);

    // Calculate the normalized latitude.
    double normalizedLatitude = (latitudeInDecimalDegrees - 75) / (-140);

    // 16800 is the amount of pixels from top to bottom on the image. That times the normalized value gives us a pixel position on the image.
    topToBottomAmoutnOfPixels = (int)(16800 * normalizedLatitude);
}
else
{
    Console.WriteLine("You must enter a valid value with the example 59N11 or 59S11 meaning 59°11' north and south respectively.");
    return;
}

// Ask user for longitude.
Console.WriteLine("What is the longitude? (ex 18E11 or 18W59)");
var longitude = Console.ReadLine();

if (longitude == null)
{
    Console.WriteLine("You must enter a valid value with the example 18E11 or 18W11 meaning 18°11' east and west respectively.");
    return;
}

if (longitude.Contains('E'))
{
    degreesAndNM = longitude.Split('E');
}
else if (longitude.Contains('W'))
{
    degreesAndNM = longitude.Split('W');
    isEastOfGreenwich = false;
}
else
{
    Console.WriteLine("You must enter a valid value with the example 18E11 or 18W11 meaning 18°11' east and west respectively.");
    return;
}

isCorrectDegrees = int.TryParse(degreesAndNM[0], out degrees);
isCorrectNM = int.TryParse(degreesAndNM[1], out nauticalMiles);
if (isCorrectDegrees && isCorrectNM)
{
    longitudeInDecimalDegrees = (double)degrees + ((double)nauticalMiles / 60);

    // Determines amount of nautical miles from Greenwich.
    degrees *= 60;
    nauticalMiles += degrees;

    // Calculate pixel amount. 2 pixels = 1 nautical mile
    int pixlesFromGreenwich = nauticalMiles * 2;
    if (isEastOfGreenwich)
    {
        leftToRightAmoutnOfPixels = (43200 / 2) + pixlesFromGreenwich;
    }
    else
    {
        leftToRightAmoutnOfPixels = (43200 / 2) - pixlesFromGreenwich;
    }
}
else
{
    Console.WriteLine("Please write the longitude correctly, as exampeled.");
    return;
}

// Call the LightPolution.cs and Weather.cs libraries to get the light polution and weather.

double kpValue;
string? clouds;
var time = DateTime.Now.Hour;
double mpsasValue;

try
{
    var lightPolutionCalculator = new LightPolutionCalculator();
    mpsasValue = lightPolutionCalculator.GetLightPolution(topToBottomAmoutnOfPixels, leftToRightAmoutnOfPixels);

    var weatherCalculator = new WeatherCalculator();
    var weather = await weatherCalculator.GetCurrentWeatherAndKP(latitudeInDecimalDegrees, longitudeInDecimalDegrees);

    if (weather != null)
    {
        kpValue = weather.KpValue;
        if (weather.Clouds != null)
        {
            clouds = weather.Clouds.ToString();
        }
        else
        {
            throw new Exception("The cloud data was null.");
        }
    }
    else
    {
        throw new Exception("The weather data was null.");
    }
}
catch (Exception e)
{
    Console.WriteLine("An error occured while trying to get the light polution or weather. Error: " + e.Message);
    return;
}

int chanceOfSeeingNorthernLights = -1;
string likelyhoodOfSeeingNorthernLights;

switch (kpValue)
{
    case 0 when latitudeInDecimalDegrees < 48 || mpsasValue < 19.50 || time > 8 && time < 16:
        chanceOfSeeingNorthernLights = 0;
        likelyhoodOfSeeingNorthernLights = $"There is a very slim chance there will be any northern lights at your location. Especially with a kp value under the max of 9 and a lightpolution of {mpsasValue}. Right now the kp value is {kpValue}.";
        break;
    case 1 when kpValue <= 4  || clouds == "overcast clouds":
        chanceOfSeeingNorthernLights = 0;
        likelyhoodOfSeeingNorthernLights = $"There is a very low or none existant chance of seeing the northern lights right now. The kp value is {kpValue} and the cloud coverage is {clouds}.";
        break;
    case 2 when kpValue > 4 && kpValue <= 6 && latitudeInDecimalDegrees > 55:
        chanceOfSeeingNorthernLights = 1;
        
        break;
    case 3 when kpValue > 6 && kpValue <= 7.5 && latitudeInDecimalDegrees > 52:
        chanceOfSeeingNorthernLights = 2;
        break;
    case 4 when kpValue < 8 && latitudeInDecimalDegrees > 50:
        chanceOfSeeingNorthernLights = 3;
        break;
    case 5 when kpValue >= 8 && latitudeInDecimalDegrees > 48:
        chanceOfSeeingNorthernLights = 4;
        break;
}

Console.WriteLine(chanceOfSeeingNorthernLights);