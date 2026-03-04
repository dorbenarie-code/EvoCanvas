using System;
using EvoCanvas.Models;

namespace EvoCanvas.Core
{
    public class ColorDistanceCalculator : IFitnessCalculator
    {
        private double CalculateDistance(Pixel p1, Pixel p2)
        {
            int rDiff = p1.R - p2.R;
            int gDiff = p1.G - p2.G;
            int bDiff = p1.B - p2.B;

            int sumOfSquares = (rDiff * rDiff) + (gDiff * gDiff) + (bDiff * bDiff);
            return Math.Sqrt(sumOfSquares);
        }

        public double CalculateAverageDistance(Pixel[] targetPixels, Pixel[] candidatePixels)
        {
            if (targetPixels.Length != candidatePixels.Length)
            {
                throw new Exception("Images must be of the same size to compare them!");
            }

            double totalDistance = 0;
            int numberOfPixels = targetPixels.Length;

            for (int i = 0; i < numberOfPixels; i++)
            {
                double pixelDistance = CalculateDistance(targetPixels[i], candidatePixels[i]);
                totalDistance += pixelDistance;
            }

            return totalDistance / numberOfPixels;
        }
    }
}