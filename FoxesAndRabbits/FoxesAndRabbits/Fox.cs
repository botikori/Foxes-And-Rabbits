namespace FoxesAndRabbits;

public class Fox : Animal
{
    public Fox(Grid grid) : base(grid)
    {
        _maxHunger = 10;
    }
}