using System;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using EvoCanvas.Models;

namespace EvoCanvas.Core
{
    public class Mutator : IMutator
    {
        private static Random rnd = new Random();

        public DNA CreateMutatedChild(DNA parent, int maxWidth, int maxHeight, double currentFitness)
        {
            DNA child = new DNA();
            foreach (PolygonGene parentGene in parent.Genes)
            {
                PolygonGene clonedGene = new PolygonGene();
                clonedGene.BrushColor = parentGene.BrushColor; 

                foreach (PointF pt in parentGene.Points)
                {
                    clonedGene.Points.Add(new PointF(pt.X, pt.Y)); 
                }
                child.Genes.Add(clonedGene);
            }

            int mutationType = rnd.Next(100);

            if (child.Genes.Count == 0 || mutationType < 15)
            {
                child.Genes.Add(GenerateRandomTriangle(maxWidth, maxHeight, currentFitness));
            }
            else if (mutationType < 17)
            {
                int randomGeneIndex = rnd.Next(child.Genes.Count);
                child.Genes.RemoveAt(randomGeneIndex);
            }
            else
            {
                int randomGeneIndex = rnd.Next(child.Genes.Count);
                PolygonGene geneToModify = child.Genes[randomGeneIndex];

                double factor = Math.Clamp(currentFitness / 100.0, 0.02, 1.0);

                if (rnd.Next(100) < 50)
                {
                    byte r = (byte)rnd.Next(256);
                    byte g = (byte)rnd.Next(256);
                    byte b = (byte)rnd.Next(256);

                    int minAlpha = Math.Max(5, (int)(50 * factor));
                    int maxAlpha = Math.Max(minAlpha + 1, (int)(200 * factor));
                    byte alpha = (byte)rnd.Next(minAlpha, maxAlpha);

                    geneToModify.BrushColor = new Rgba32(r, g, b, alpha);
                }
                else
                {
                    int randomPointIndex = rnd.Next(geneToModify.Points.Count);
                    PointF pt = geneToModify.Points[randomPointIndex];

                    int maxDelta = Math.Max(1, (int)(25 * factor));
                    float deltaX = rnd.Next(-maxDelta, maxDelta + 1);
                    float deltaY = rnd.Next(-maxDelta, maxDelta + 1);

                    float newX = pt.X + deltaX;
                    float newY = pt.Y + deltaY;

                    newX = Math.Clamp(newX, 0, maxWidth);
                    newY = Math.Clamp(newY, 0, maxHeight);

                    geneToModify.Points[randomPointIndex] = new PointF(newX, newY);
                }
            }

            return child;
        }

        private PolygonGene GenerateRandomTriangle(int maxWidth, int maxHeight, double currentFitness)
        {
            PolygonGene triangle = new PolygonGene();

            double factor = Math.Clamp(currentFitness / 100.0, 0.02, 1.0);

            byte r = (byte)rnd.Next(256);
            byte g = (byte)rnd.Next(256);
            byte b = (byte)rnd.Next(256);
            
            int minAlpha = Math.Max(5, (int)(50 * factor));
            int maxAlpha = Math.Max(minAlpha + 1, (int)(200 * factor));
            byte alpha = (byte)rnd.Next(minAlpha, maxAlpha);
            
            int radius = Math.Max(3, (int)((maxWidth / 2.0) * factor));

            triangle.BrushColor = new Rgba32(r, g, b, alpha);

            float centerX = rnd.Next(maxWidth);
            float centerY = rnd.Next(maxHeight);

            for (int i = 0; i < 3; i++)
            {
                float offsetX = rnd.Next(-radius, radius + 1);
                float offsetY = rnd.Next(-radius, radius + 1);

                float x = Math.Clamp(centerX + offsetX, 0, maxWidth);
                float y = Math.Clamp(centerY + offsetY, 0, maxHeight);

                triangle.Points.Add(new PointF(x, y));
            }

            return triangle;
        }
    }
}