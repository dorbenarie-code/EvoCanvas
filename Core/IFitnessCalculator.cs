using EvoCanvas.Models;
namespace EvoCanvas.Core
{
    public interface IFitnessCalculator
    {
        double CalculateAverageDistance(Pixel[] targetPixels, Pixel[] candidatePixels);
    }
}