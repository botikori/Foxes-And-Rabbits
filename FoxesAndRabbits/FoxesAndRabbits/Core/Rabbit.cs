using System.Numerics;

namespace FoxesAndRabbits.Core;

public class Rabbit : Animal
{
    public static int CalculateFoodValue(Grass grass)
    {
        if (grass == Grass.High)
            return 2;
        if (grass == Grass.Medium)
            return 1;
        return 0;
    }

    public Rabbit(Grid grid, Vector2 startPosition) : base(grid, startPosition)
    {
        MaxHunger = 5;
        CurrentHunger = MaxHunger;
        Statistic.NumberOfRabbits++;
    }
}