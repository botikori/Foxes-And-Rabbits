namespace FoxesAndRabbits.Core;

public class Rabbit : Animal
{
    public static int DetectRange;
    private const int MinDetectRange = 1;
    private const int MaxDetectRange = 5;
    public Rabbit(Grid grid) : base(grid)
    {
        _maxHunger = 5;
    }

    public static void SetRabbitDetectRange(int range)
    {
        if (range > MaxDetectRange || range < MinDetectRange) 
        {
            throw new ArgumentOutOfRangeException();
        }
        DetectRange = range;
    }

    protected int CalculateFoodValue(Grass grass)
    {
        if (grass == Grass.High)
            return 2;
        if (grass == Grass.Medium)
            return 1;
        return 0;
    }
}