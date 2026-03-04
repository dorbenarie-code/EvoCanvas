using System.Collections.Generic;
using SixLabors.ImageSharp; 
using SixLabors.ImageSharp.PixelFormats;

namespace EvoCanvas.Models 
{
    public class PolygonGene
    {
        public List<PointF> Points { get; set; }

        public Rgba32 BrushColor { get; set; }

        public PolygonGene()
        {
            Points = new List<PointF>();
        }
    }
}