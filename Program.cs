using System;
using EvoCanvas.Models;
using EvoCanvas.Core;

namespace EvoCanvas
{
    class Program
    {
        private static bool keepRunning = true;

        static void Main(string[] args)
        {
            Console.WriteLine("Starting the Evolutionary Engine (Scale 200x200)...");
            Console.WriteLine("Press Ctrl+C to stop evolution and save the current result.");
            Console.WriteLine("-----------------------------------");

            Console.CancelKeyPress += delegate (object sender, ConsoleCancelEventArgs e)
            {
                e.Cancel = true;
                keepRunning = false;
                Console.WriteLine("\nStopping evolution gracefully...");
            };

            try
            {
                string imagePath = "monalisa.jpg";
                int width = 200;
                int height = 200;
                
                IImageToPixelConverter converter = new ImageToPixelConverter();
                IShapeRenderer renderer = new ShapeRenderer();
                IFitnessCalculator calculator = new ColorDistanceCalculator();
                IMutator mutator = new Mutator();

                Pixel[] targetPixels = converter.ConvertToPixelArray(imagePath, width, height);
                
                DNA parentDna = new DNA();
                Pixel[] parentPixels = renderer.RenderDNA(parentDna, width, height);
                parentDna.Fitness = calculator.CalculateAverageDistance(targetPixels, parentPixels);
                
                Console.WriteLine($"Generation 0 - Initial Fitness (Black Canvas): {parentDna.Fitness:F2}");
                
                int totalGenerations = 3000000;
                Console.WriteLine($"Starting Evolution ({totalGenerations} Generations max)...\n");
                
                for (int generation = 1; generation <= totalGenerations && keepRunning; generation++)
                {
                    DNA childDna = mutator.CreateMutatedChild(parentDna, width, height, parentDna.Fitness);
                    
                    Pixel[] childPixels = renderer.RenderDNA(childDna, width, height);
                    
                    childDna.Fitness = calculator.CalculateAverageDistance(targetPixels, childPixels);
                    
                    if (childDna.Fitness < parentDna.Fitness)
                    {
                        parentDna = childDna;
                    }

                    if (generation % 100 == 0)
                    {
                        Console.WriteLine($"Gen {generation} | Current Best Fitness: {parentDna.Fitness:F2} | Total Polygons: {parentDna.Genes.Count}");
                    }
                }
                
                Console.WriteLine("\n-----------------------------------");
                Console.WriteLine($"Evolution Complete or Stopped! Final Fitness: {parentDna.Fitness:F2}");

                renderer.SaveDNAToFile(parentDna, width, height, "MyMonaLisa.png");
                Console.WriteLine("Image saved successfully as 'MyMonaLisa.png'");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}