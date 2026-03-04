using System.Collections.Generic;

namespace EvoCanvas.Models
{
    public class DNA
    {
        public List<PolygonGene> Genes { get; set; }

        public double Fitness { get; set; }

        public DNA()
        {
            Genes = new List<PolygonGene>();
            Fitness = double.MaxValue;
        }
    }
}