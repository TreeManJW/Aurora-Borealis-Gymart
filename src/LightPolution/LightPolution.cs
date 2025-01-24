using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

// TO DO:
// - Fix out of bounds coordinates
// - Add explanations to code

namespace LightPolution;

public class LightPolutionCalculator
{
    public double GetLightPolution(int latitudeInPixels, int longitudeInPixels)
    {
        double? mpsasValue = null;

        try
        {
            Rgb24 lightPolutionColor = ColorFinder(latitudeInPixels, longitudeInPixels);
            mpsasValue = mpsasValueFromRGB(lightPolutionColor);
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to get color on map or mpsas value with exception: " + ex.Message);
        }
        if (mpsasValue != null)
        {
            return mpsasValue.Value;
        }
        else
        {
            throw new Exception("Failed to get mpsas value from RGB value. No value was ever assigned.");
        }
    }
    static Rgb24 ColorFinder(int latitudePixels, int longitudePixels)
    {
        Rgb24 color = new Rgb24(0, 0, 0);
        Image<Rgb24> image;
        try
        {
            using (image = Image.Load<Rgb24>("world2022.png"))
            {
                color = image[longitudePixels, latitudePixels];

                image[longitudePixels, latitudePixels] = new Rgb24(255, 0, 0);
                image.Save("debugOutput.png");
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to find color on image with exception: " + ex.Message);
        }
        return color;
    }
    static double mpsasValueFromRGB(Rgb24 RGBValueFromMap)
    {
        Dictionary<Rgb24, double> mpsasAndRGBValues = new Dictionary<Rgb24, double>();
        double[] mpsasValues = new double[14] { 22.00, 21.95, 21.85, 21.75, 21.60, 21.35, 21.10, 20.70, 20.25, 19.70, 19.75, 19.25, 18.60, 18.05 };
        try
        {
            using (Image<Rgb24> image = Image.Load<Rgb24>("colorbar.png"))
            {
                for (int i = 0; i < 14; i++)
                {
                    // The color bar colors are aproximately 406 pixels wide. The first color starts at 200 (the middle of the first color) and then the program itterates through all 14 colors.
                    Rgb24 color = image[200 + (i * 406), 600];
                    mpsasAndRGBValues.Add(color, mpsasValues[i]);
                }
            }
            if (mpsasAndRGBValues.TryGetValue(RGBValueFromMap, out double mpsasValue))
            {
                return mpsasValue;
            }
            else
            {
                throw new Exception("The color was not found in the color bar.");
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to get mpsasvalue with error: " + ex.Message);
        }
    }
}
