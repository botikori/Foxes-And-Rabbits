using System.Numerics;
using System.Linq;

namespace FoxesAndRabbits.Core;

public class Simulation
{
    private Grid _grid;
    private Random _random;
    private int _rabbitCount = 2;
    private int _foxCount = 2;
    
    
    public Simulation(Grid grid, int rabbitCount, int foxCount)
    {
        this._grid = grid;
        this._rabbitCount = rabbitCount;
        this._foxCount = foxCount;
        
        _random = new Random();
    }

    public void Tick()
    {
        foreach (var cell in _grid.Cells)
        {
            if (cell.AnimalStandingOnCell != null)
            {
                cell.AnimalStandingOnCell.Step();
            }
        }
    }

    public void StartSimulation()
    {
        StartSimulation(GenerateRandomPositions().first, GenerateRandomPositions().second);
    }
    
    private (List<Vector2> first, List<Vector2> second) GenerateRandomPositions()
    {
        List<Vector2> randomPositions = new List<Vector2>();
        
        int i = 0;
        
        while (i < _rabbitCount + _foxCount)
        {
            Vector2 randomPosition = new Vector2(_random.Next(0, _grid.Width), _random.Next(0, _grid.Height));
            
            if (!randomPositions.Contains(randomPosition))
            {
                randomPositions.Add(randomPosition);
                i++;    
            }
            
        }

        return (randomPositions.Take(_rabbitCount).ToList(), randomPositions.Skip(_rabbitCount).ToList());
    }

    public void StartSimulation(List<Vector2> rabbitPositions, List<Vector2> foxPositions)
    {
        foreach (var rabbitPosition in rabbitPositions)
        {
            Rabbit currentRabbit = new Rabbit(_grid, rabbitPosition);
        }

        foreach (var foxPosition in foxPositions)
        {
            Fox currentFox = new Fox(_grid, foxPosition);
        }
    }

    public bool IsGivenPositionCorrect(string[] pos)
    {
        return pos.Length == 2 
               && int.TryParse(pos[0], out var tempx)
               && int.TryParse(pos[1], out var tempy)
               && tempx < _grid.Width
               && tempy < _grid.Height
               && tempx >= 0
               && tempy >= 0;
    }
}