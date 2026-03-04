using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Drawing.Processing;
using EvoCanvas.Models;

namespace EvoCanvas.Core
{
    public class ShapeRenderer : IShapeRenderer
    {
        public Pixel[] RenderDNA(DNA dna, int width, int height)
        {
            using (Image<Rgba32> canvas = new Image<Rgba32>(width, height, Color.Black))
            {
                foreach (PolygonGene gene in dna.Genes)
                {
                    PointF[] pointsArray = gene.Points.ToArray();
                    canvas.Mutate(x => x.FillPolygon(gene.BrushColor, pointsArray));
                }

                Pixel[] pixelArray = new Pixel[width * height];
                int currentIndex = 0;

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        Rgba32 color = canvas[x, y];
                        pixelArray[currentIndex] = new Pixel(color.R, color.G, color.B);
                        currentIndex++;
                    }
                }

                return pixelArray;
            }
        }

        public void SaveDNAToFile(DNA dna, int width, int height, string outputPath)
        {
            using (Image<Rgba32> canvas = new Image<Rgba32>(width, height, Color.Black))
            {
                foreach (PolygonGene gene in dna.Genes)
                {
                    PointF[] pointsArray = gene.Points.ToArray();
                    canvas.Mutate(x => x.FillPolygon(gene.BrushColor, pointsArray));
                }

                canvas.Save(outputPath);
            }
        }
    }
}