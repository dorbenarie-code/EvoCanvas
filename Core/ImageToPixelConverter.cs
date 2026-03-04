using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing; 
using EvoCanvas.Models;

namespace EvoCanvas.Core
{
    public class ImageToPixelConverter : IImageToPixelConverter
    {
        public Pixel[] ConvertToPixelArray(string imagePath, int targetWidth, int targetHeight)
        {
            using (Image<Rgba32> image = Image.Load<Rgba32>(imagePath))
            {
                image.Mutate(x => x.Resize(targetWidth, targetHeight));

                int totalPixels = targetWidth * targetHeight;
                Pixel[] pixelArray = new Pixel[totalPixels];
                int currentIndex = 0;

                for (int y = 0; y < targetHeight; y++)
                {
                    for (int x = 0; x < targetWidth; x++)
                    {
                        Rgba32 color = image[x, y];
                        pixelArray[currentIndex] = new Pixel(color.R, color.G, color.B);
                        currentIndex++;
                    }
                }

                return pixelArray;
            }
        }
    }
}