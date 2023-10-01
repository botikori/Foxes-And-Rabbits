using System.Numerics;

namespace FoxesAndRabbits.Core;

public class Simulation
{
    private Grid _grid;
    private Random _random;
    private int _animalCount = 2;
    
    public Simulation(Grid grid, int animalCount)
    {
        this._grid = grid;
        this._animalCount = animalCount;
        
        _random = new Random();
    }
    
    public void StartSimulation()
    {
        List<Vector2> randomPositions = new List<Vector2>();
        
        int i = 0;
        
        while (i < _animalCount)
        {
            Vector2 randomPosition = new Vector2(_random.Next(0, _grid.Width), _random.Next(0, _grid.Height));
            
            if (!randomPositions.Contains(randomPosition))
            {
                randomPositions.Add(randomPosition);
                i++;    
            }
            
        }
        
        StartSimulation(randomPositions);
    }

    public void StartSimulation(List<Vector2> positions)
    {
        foreach (var position in positions)
        {

        }
    }
}