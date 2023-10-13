using System.Numerics;
using FoxesAndRabbits.Core.Exceptions;

namespace FoxesAndRabbits.Core;

public class Fox : Animal
{
    private static int DetectRange;
    private const int MinDetectRange = 2;
    private const int MaxDetectRange = 6;
    
    public static void SetFoxDetectRange(int range)
    {
        if (range > MaxDetectRange || range < MinDetectRange) 
        {
            throw new ArgumentOutOfRangeException("Fox detection range must be between 2 and 6!");
        }

        if (Rabbit.DetectRange >= range)
        {
            throw new FoxRangeTooLowException();
        }
    }

    public Fox(Grid grid, Vector2 startPosition) : base(grid, startPosition)
    {
        _maxHunger = 10;
        Statistic.numberOfFoxes++;
    }
}