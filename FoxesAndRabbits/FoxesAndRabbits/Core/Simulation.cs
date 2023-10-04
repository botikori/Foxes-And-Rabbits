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
        StartSimulation(GenerateRandomPositions().first, GenerateRandomPositions().second);
    }
    
    private (List<Vector2> first, List<Vector2> second) GenerateRandomPositions()
    {
        List<Vector2> randomPositions = new List<Vector2>();
        
        int i = 0;
        
        while (i < _animalCount * 2)
        {
            Vector2 randomPosition = new Vector2(_random.Next(0, _grid.Width), _random.Next(0, _grid.Height));
            
            if (!randomPositions.Contains(randomPosition))
            {
                randomPositions.Add(randomPosition);
                i++;    
            }
            
        }

        return (randomPositions.Take(_animalCount).ToList(), randomPositions.Skip(_animalCount).ToList());
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
        int temp;
        if (pos.Length != 2  || !int.TryParse(pos[0], out temp) || !int.TryParse(pos[1], out temp))
        {
            return false;
        }
        return true;
    }
}