namespace FoxesAndRabbits;

public class Rabbit : Animal
{
    public Rabbit(Grid grid) : base(grid)
    {
        _hunger = 5;
        _stepDistance = 1;
    }
}