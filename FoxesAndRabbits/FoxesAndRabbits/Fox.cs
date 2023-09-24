namespace FoxesAndRabbits;

public class Fox : Animal
{
    public Fox(Grid grid) : base(grid)
    {
        _hunger = 10;
        _stepDistance = 1;
    }
}