using EvoCanvas.Models;

namespace EvoCanvas.Core
{
    public interface IShapeRenderer
    {
        Pixel[] RenderDNA(DNA dna, int width, int height);
        void SaveDNAToFile(DNA dna, int width, int height, string outputPath);
    }
}