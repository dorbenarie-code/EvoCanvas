using EvoCanvas.Models;
namespace EvoCanvas.Core
{
    public interface IMutator
    {
        DNA CreateMutatedChild(DNA parent, int maxWidth, int maxHeight, double currentFitness);
    }
}