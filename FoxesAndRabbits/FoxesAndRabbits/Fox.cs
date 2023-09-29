namespace FoxesAndRabbits;

public class Fox : Animal
{
    private static int DetectRange;
    private const int MinDetectRange = 2;
    private const int MaxDetectRange = 6;
    public Fox(Grid grid) : base(grid)
    {
        _maxHunger = 10;
    }
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
}