using EvoCanvas.Models;

namespace EvoCanvas.Core
{
    public interface IImageToPixelConverter
    {
        Pixel[] ConvertToPixelArray(string imagePath, int targetWidth, int targetHeight);
    }
}