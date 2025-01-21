using LightPolution;
using Weather;


// Ask user for latitude, parese it and check if it is valid
//var latitude = 5.5;
// Ask user for longitude, parese it and check if it is valid
//var longitude = 2.2;

var lightPolutionCalculator = new LightPolutionCalculator();
var lightPolution = lightPolutionCalculator.GetLightPolution(latitude, longitude);

var weatherCalculator = new WeatherCalculator();
var weather = weatherCalculator.GetCurrentWeather(latitude, longitude);

// Call Nasa API to get KP index (int)

var time = DateTime.Now;

// Call custom library to get the chance of seeing the northern lights (double)

// Print the results

string[] degreesAndNM = ["PlaceHolder", "PlaceHolder1"];
bool isEastOfGreenwich = true;
bool isNorthOfEquator = true;

Console.WriteLine("What is the longitude? (ex 18E11 or 18W59)");
var longitude = Console.ReadLine();

if (longitude == null)
{
    throw new ArgumentException("You must enter a valid value");
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
    throw new ArgumentException("You must enter a valid value with the example 18E11 or 18W11 meaning 18°11' east and west respectively.");
}

bool isCorrectDegrees = int.TryParse(degreesAndNM[0], out int degrees);
bool isCorrectNM = int.TryParse(degreesAndNM[1], out int nauticalMiles);
if (isCorrectDegrees && isCorrectNM)
{
    degrees *= 60;
    nauticalMiles += degrees;
    Console.WriteLine("Total nautical miles of that position from Greenwich is " + nauticalMiles);

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
    Console.WriteLine("Amout of pixels left to eight on the image = " + leftToRightAmoutnOfPixels);
}
else
{
    Console.WriteLine("Please write the longitude correctly, as exampeled.");
    Task.Delay(1000);
    Environment.Exit(1);
}

Console.WriteLine("What is the longitude? (ex 59N11 or 59S11 Note: Between 75N and 65S)");
var latitude = Console.ReadLine();
if (latitude == null)
{
    throw new ArgumentException("You must enter a valid value");
}

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
    throw new ArgumentException("You must enter a valid value with the example 59N11 or 59S11 meaning 59°11' north and south respectively.");
}

isCorrectDegrees = int.TryParse(degreesAndNM[0], out degrees);
isCorrectNM = int.TryParse(degreesAndNM[1], out nauticalMiles);
if (!isNorthOfEquator)
{
    degrees = -degrees;
    nauticalMiles = -nauticalMiles;
}
if (isCorrectDegrees && isCorrectNM)
{
    double degreesInDecimals = (double)degrees + ((double)nauticalMiles / 60);

    double normalizedLatitude = (degreesInDecimals - 75) / (-140);
    topToBottomAmoutnOfPixels = (int)(16800 * normalizedLatitude);

    Console.WriteLine("Amount of pixels from top to bottom on the picture is: " + topToBottomAmoutnOfPixels);
}
else
{
    throw new ArgumentException("You must enter a valid value with the example 59N11 or 59S11 meaning 59°11' north and south respectively.");
}
if (leftToRightAmoutnOfPixels == 0)
{
    leftToRightAmoutnOfPixels += 1;
}
else if (leftToRightAmoutnOfPixels == 43200)
{
    leftToRightAmoutnOfPixels -= 1;
}
Console.WriteLine(leftToRightAmoutnOfPixels);
return (leftToRightAmoutnOfPixels, topToBottomAmoutnOfPixels);
    }
    static Rgb24 ColorFinder(int longitudePixels, int latitudePixels)
{
    Rgb24 color = new Rgb24(0, 0, 0);
    Image<Rgb24> image;
    try
    {
        using (image = Image.Load<Rgb24>("world2022.png"))
        {
            color = image[longitudePixels, latitudePixels];

            for (int y = 0; y < 20; y++)
            {
                for (int x = 0; x < 20; x++)
                {
                    image[longitudePixels + (10 - y), latitudePixels + (10 - x)] = new Rgb24(193, 64, 193);
                }
            }
            image.Save("output.png");
        }
    }
    catch (Exception ex)
    {
        throw new Exception("Failed to find color on image with exception: " + ex.Message);
    }