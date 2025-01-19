using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

// TO DO:
// - Fix out of bounds coordinates
// - Add explanations to code

namespace LightPolution;

public class LightPolutionCalculator
{
    public double GetLightPolution(double latitude, double longitude)
    {
        return 0;
    }


    //     int latPixel = 1;
    //     int longPixel = 1;
    //     double? mpsasValue = null;
    //         try
    //         {
    //         (longPixel, latPixel) = LongitudeLatitudeInPixels();
    //     Rgb24 lightPolutionColor = ColorFinder(longPixel, latPixel);
    //     Console.WriteLine("Collor found at that place: " + lightPolutionColor);
    //         mpsasValue = mpsasValueFromRGB(lightPolutionColor);

    //         catch(Exception ex)
    //         {
    //             Console.WriteLine(ex.Message);
    //             Console.WriteLine("Press 'e' to exit. Press any other button to continue.");
    //             var keyPress = Console.ReadKey();
    //             if (keyPress.KeyChar == 'e' || keyPress.KeyChar == 'E'){
    //                 Environment.Exit(0);
    //             }
    //             else
    // {
    //     Console.WriteLine("\nContinuing program");
    //     Main();
    // }
    //         }
    //         if (mpsasValue != null)
    // {
    //     Console.WriteLine("The mpsas value at that location is: " + mpsasValue);
    // }
    // else
    // {
    //     Console.WriteLine("The mpsas value was not found.");
    // }
    //     }
    //     static (int, int) LongitudeLatitudeInPixels()
    // {
    //     int leftToRightAmoutnOfPixels = 0;
    //     int topToBottomAmoutnOfPixels = 0;

    //     string[] degreesAndNM = ["PlaceHolder", "PlaceHolder1"];
    //     bool isEastOfGreenwich = true;
    //     bool isNorthOfEquator = true;

    //     Console.WriteLine("What is the longitude? (ex 18E11 or 18W59)");
    //     var longitude = Console.ReadLine();

    //     if (longitude == null)
    //     {
    //         throw new ArgumentException("You must enter a valid value");
    //     }

    //     if (longitude.Contains('E'))
    //     {
    //         degreesAndNM = longitude.Split('E');
    //     }
    //     else if (longitude.Contains('W'))
    //     {
    //         degreesAndNM = longitude.Split('W');
    //         isEastOfGreenwich = false;
    //     }
    //     else
    //     {
    //         throw new ArgumentException("You must enter a valid value with the example 18E11 or 18W11 meaning 18°11' east and west respectively.");
    //     }

    //     bool isCorrectDegrees = int.TryParse(degreesAndNM[0], out int degrees);
    //     bool isCorrectNM = int.TryParse(degreesAndNM[1], out int nauticalMiles);
    //     if (isCorrectDegrees && isCorrectNM)
    //     {
    //         degrees *= 60;
    //         nauticalMiles += degrees;
    //         Console.WriteLine("Total nautical miles of that position from Greenwich is " + nauticalMiles);

    //         // Calculate pixel amount. 2 pixels = 1 nautical mile
    //         int pixlesFromGreenwich = nauticalMiles * 2;
    //         if (isEastOfGreenwich)
    //         {
    //             leftToRightAmoutnOfPixels = (43200 / 2) + pixlesFromGreenwich;
    //         }
    //         else
    //         {
    //             leftToRightAmoutnOfPixels = (43200 / 2) - pixlesFromGreenwich;
    //         }
    //         Console.WriteLine("Amout of pixels left to eight on the image = " + leftToRightAmoutnOfPixels);
    //     }
    //     else
    //     {
    //         Console.WriteLine("Please write the longitude correctly, as exampeled.");
    //         Task.Delay(1000);
    //         Environment.Exit(1);
    //     }

    //     Console.WriteLine("What is the longitude? (ex 59N11 or 59S11 Note: Between 75N and 65S)");
    //     var latitude = Console.ReadLine();
    //     if (latitude == null)
    //     {
    //         throw new ArgumentException("You must enter a valid value");
    //     }

    //     if (latitude.Contains('N'))
    //     {
    //         degreesAndNM = latitude.Split('N');
    //     }
    //     else if (latitude.Contains('S'))
    //     {
    //         degreesAndNM = latitude.Split('S');
    //         isNorthOfEquator = false;
    //     }
    //     else
    //     {
    //         throw new ArgumentException("You must enter a valid value with the example 59N11 or 59S11 meaning 59°11' north and south respectively.");
    //     }

    //     isCorrectDegrees = int.TryParse(degreesAndNM[0], out degrees);
    //     isCorrectNM = int.TryParse(degreesAndNM[1], out nauticalMiles);
    //     if (!isNorthOfEquator)
    //     {
    //         degrees = -degrees;
    //         nauticalMiles = -nauticalMiles;
    //     }
    //     if (isCorrectDegrees && isCorrectNM)
    //     {
    //         double degreesInDecimals = (double)degrees + ((double)nauticalMiles / 60);

    //         double normalizedLatitude = (degreesInDecimals - 75) / (-140);
    //         topToBottomAmoutnOfPixels = (int)(16800 * normalizedLatitude);

    //         Console.WriteLine("Amount of pixels from top to bottom on the picture is: " + topToBottomAmoutnOfPixels);
    //     }
    //     else
    //     {
    //         throw new ArgumentException("You must enter a valid value with the example 59N11 or 59S11 meaning 59°11' north and south respectively.");
    //     }
    //     if (leftToRightAmoutnOfPixels == 0)
    //     {
    //         leftToRightAmoutnOfPixels += 1;
    //     }
    //     else if (leftToRightAmoutnOfPixels == 43200)
    //     {
    //         leftToRightAmoutnOfPixels -= 1;
    //     }
    //     Console.WriteLine(leftToRightAmoutnOfPixels);
    //     return (leftToRightAmoutnOfPixels, topToBottomAmoutnOfPixels);
    // }
    // static Rgb24 ColorFinder(int longitudePixels, int latitudePixels)
    // {
    //     Rgb24 color = new Rgb24(0, 0, 0);
    //     Image<Rgb24> image;
    //     try
    //     {
    //         using (image = Image.Load<Rgb24>("world2022.png"))
    //         {
    //             color = image[longitudePixels, latitudePixels];

    //             for (int y = 0; y < 20; y++)
    //             {
    //                 for (int x = 0; x < 20; x++)
    //                 {
    //                     image[longitudePixels + (10 - y), latitudePixels + (10 - x)] = new Rgb24(193, 64, 193);
    //                 }
    //             }
    //             image.Save("output.png");
    //         }
    //     }
    //     catch (Exception ex)
    //     {
    //         throw new Exception("Failed to find color on image with exception: " + ex.Message);
    //     }

    //     return color;
    // }
    // static double mpsasValueFromRGB(Rgb24 RGBValueFromMap)
    // {
    //     Dictionary<Rgb24, double> mpsasAndRGBValues = new Dictionary<Rgb24, double>();
    //     double[] mpsasValues = new double[14] { 22.00, 21.95, 21.85, 21.75, 21.60, 21.35, 21.10, 20.70, 20.25, 19.70, 19.75, 19.25, 18.60, 18.05 };
    //     try
    //     {
    //         using (Image<Rgb24> image = Image.Load<Rgb24>("colorbar.png"))
    //         {
    //             for (int i = 0; i < 14; i++)
    //             {
    //                 // The color bar colors are aproximately 406 pixels wide. The first color starts at 200 (the middle of the first color) and then the program itterates through all 14 colors.
    //                 Rgb24 color = image[200 + (i * 406), 600];
    //                 mpsasAndRGBValues.Add(color, mpsasValues[i]);
    //             }
    //         }
    //         if (mpsasAndRGBValues.TryGetValue(RGBValueFromMap, out double mpsasValue))
    //         {
    //             return mpsasValue;
    //         }
    //         else
    //         {
    //             throw new Exception("The color was not found in the color bar.");
    //         }
    //     }
    //     catch (Exception ex)
    //     {
    //         throw new Exception("It did nor work with exception: " + ex.Message);
    //     }
}
