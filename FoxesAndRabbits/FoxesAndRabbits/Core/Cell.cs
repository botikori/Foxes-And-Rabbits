namespace FoxesAndRabbits.Core;

public class Cell
{
    public Grass GrassState { get; set; }
    public int XPos { get; set; }
    public int YPos { get; set; }

    public Animal? AnimalStandingOnCell { get; set; } = null;

    public void SetRandomGrassState()
    {
        Random random = new Random();
        int chance = random.Next(100)+1;
        if (chance <= 70)
            GrassState = Grass.Low;
        else if (chance <= 90)
            GrassState = Grass.Medium;
        else
            GrassState = Grass.High;
    }
    public Cell(int x, int y)
    {
        XPos = x;
        YPos = y;
        SetRandomGrassState();
    }

}