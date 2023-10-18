using FoxesAndRabbits.Core;
namespace FoxesAndRabbits.UI;

public class DrawGrid
{
    private readonly Grid _grid;
   
    public DrawGrid(Grid grid)
    {
        _grid = grid;
    }
    
    public void Draw()
    {
        
        Dictionary<Grass, string> symbols = new Dictionary<Grass, string>()
        {
            {Grass.Low,"O"},
            {Grass.Medium,"X"},
            {Grass.High, "#"}
        };
        for (int x = 0; x < _grid.Width; x++)
        {
            Console.Write("| ");
            for (int y = 0; y < _grid.Height; y++)
            {
                Animal? currentanimal = _grid.GetCellAtPosition(x, y).AnimalStandingOnCell;
                if (currentanimal is not null)
                {
                    if (currentanimal is Fox)
                    {
                        Console.Write($"{ConsoleColors.RED}F{ConsoleColors.NORMAL} | ");
                    }
                    else
                        Console.Write($"{ConsoleColors.YELLOW}R{ConsoleColors.NORMAL} | ");
                }
                else
                    Console.Write($"{ConsoleColors.GREEN}{symbols[_grid.Cells[x,y].GrassState]}{ConsoleColors.NORMAL} | ");
            }
            Console.WriteLine();
        }
    }

    public void UpdateGrid()
    {
        Console.Clear();
        Draw();
    }
}