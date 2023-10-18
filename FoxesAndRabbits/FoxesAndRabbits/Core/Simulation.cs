using System.Diagnostics;
using System.Numerics;
using System.Linq;
using FoxesAndRabbits.UI;

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

    public Simulation(Grid grid)
    {
        _grid = grid;
        
        _random = new Random();
    }

    public void Tick()
    {
        List<Animal> animalsToStep = _grid.Cells.Cast<Cell>().Where(x => x.AnimalStandingOnCell != null).Select(y => y.AnimalStandingOnCell).ToList();

        foreach (var animal in animalsToStep)
        {
            animal.DecreaseHunger();
            animal.Step();
        }
        
        foreach (var cell in _grid.Cells)
        {
            if (cell.AnimalStandingOnCell == null)
            {
                Grass currentState = cell.GrassState;
                if (currentState == Grass.Medium) cell.GrassState = Grass.High;
                else if (currentState == Grass.Low) cell.GrassState = Grass.Medium;
            }
        }
        Statistic.numberOfRounds++;

        foreach (var cell in _grid.Cells)
        {
            if (cell.AnimalStandingOnCell is { IsBreeding: true })
            {
                cell.AnimalStandingOnCell.IsBreeding = false;
            }
        }
    }

    
    public bool EndOfSimulation() => Statistic.numberOfRabbits <= 1 || Statistic.numberOfFoxes <= 1;
    
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

    public void SaveGame()
    {
        Dictionary<Grass, string> symbols = new Dictionary<Grass, string>()
        {
            {Grass.Low,"O"},
            {Grass.Medium,"X"},
            {Grass.High, "#"}
        };
        
        StreamWriter w = new StreamWriter("game.txt");
        List<int> hungerValues = new List<int>();
        w.WriteLine(_grid.Width);
        w.WriteLine(_grid.Height);
        foreach (var cell in _grid.Cells)
        {
            
            if (cell.AnimalStandingOnCell != null)
            {
                hungerValues.Add(cell.AnimalStandingOnCell.GetCurrentHunger());
                switch (cell.AnimalStandingOnCell)
                {
                    case Rabbit:
                        w.Write("R");
                        break;
                    case Fox:
                            w.Write("F");
                            break;
                }
            }
            else w.Write(symbols[cell.GrassState]);
        }
        w.WriteLine();
        w.WriteLine(string.Join(",",hungerValues.Select(x=>x)));
        w.Write($"{Statistic.numberOfFoxes},{Statistic.numberOfRabbits},{Statistic.numberOfRounds},{Statistic.numberOfDeaths}");
        w.Close();
    }

    public void LoadSimulation()
    {
        StreamReader streamReader = new StreamReader("game.txt");
        int width = int.Parse(streamReader.ReadLine());
        int height = int.Parse(streamReader.ReadLine());

        string gridText = streamReader.ReadLine();
        
        _grid.CreateGrid(width, height);
        int index = 0;
        
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (gridText != null)
                {
                    _grid.GetCellAtPosition(i, j).GrassState = gridText[index] switch
                    {
                        'O' => Grass.Low,
                        'X' => Grass.Medium,
                        '#' => Grass.High,
                        _ => Grass.Low
                    };

                    if (gridText[index] == 'R')
                    {
                        _grid.GetCellAtPosition(i, j).AnimalStandingOnCell = new Rabbit(_grid, new Vector2(i, j));
                    }
                    if (gridText[index] == 'F')
                    {
                        _grid.GetCellAtPosition(i, j).AnimalStandingOnCell = new Fox(_grid, new Vector2(i, j));
                    }
                }

                index++;
            }
        }
        
        List<Animal> animalsToSetFeedLevels = _grid.Cells.Cast<Cell>().Where(x => x.AnimalStandingOnCell != null).Select(y => y.AnimalStandingOnCell).ToList();
        
        int[] feedLevels = Array.ConvertAll(streamReader.ReadLine().Split(","), s => int.Parse(s));

        int index2 = 0;
        
        foreach (var animal in animalsToSetFeedLevels)
        {
            animal.SetCurrentHunger(feedLevels[index2]);
            index2++;
        }
        streamReader.Close();
    }
}