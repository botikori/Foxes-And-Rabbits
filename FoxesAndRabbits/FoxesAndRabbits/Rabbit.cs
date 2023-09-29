namespace FoxesAndRabbits;

public class Rabbit : Animal
{
    public Rabbit(Grid grid) : base(grid)
    {
        _maxHunger = 5;
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