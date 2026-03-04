# EvoCanvas

A C# console application that recreates target images using an evolutionary algorithm. Instead of manipulating individual pixels, the engine approximates the image using only overlapping, semi-transparent polygons.

This was built as a personal project to explore genetic algorithms, mathematical optimization, and clean software architecture.

## How It Works

The engine uses a (1+1) Evolution Strategy (a form of stochastic hill climbing):
1. It starts with a blank black canvas.
2. In each generation, it mutates the current best image by adding, removing, or slightly modifying a single polygon (changing its color, transparency, or vertex positions).
3. It calculates the "fitness" (the average RGB color distance between the generated image and the target image).
4. If the mutation improves the image, it becomes the new baseline. If not, it is discarded.

## Key Features

* **Simulated Annealing:** The mutation logic dynamically adapts. In early stages, it makes large, broad changes. As the fitness score improves, it scales down automatically to apply "micro-mutations" for fine detailing.
* **Self-Cleaning (Pruning):** The algorithm has a small probability to remove polygons. If removing a hidden or useless polygon doesn't hurt the fitness score, it stays deleted, keeping the rendering efficient.
* **Clean Architecture:** Built with SOLID principles and YAGNI in mind. The core evolutionary loop is completely decoupled from the rendering engine and fitness calculations using clear interfaces.

## Tech Stack

* **Language:** C# / .NET
* **Graphics Library:** SixLabors.ImageSharp (for fast, cross-platform, headless polygon rendering)

## Getting Started

1. Clone this repository.
2. Ensure you have the .NET SDK installed.
3. Place a target image (e.g., `monalisa.jpg`) in the project directory.
4. Run the project from your terminal:
   ```bash
   dotnet run
5. Press Ctrl+C at any time to gracefully stop the evolution loop and save the current result as an image file.

---

###