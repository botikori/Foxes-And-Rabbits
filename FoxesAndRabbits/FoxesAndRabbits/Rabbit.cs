namespace FoxesAndRabbits;

public class Rabbit : Animal
{
    public Rabbit(Grid grid) : base(grid)
    {
        _maxHunger = 5;
    }
}