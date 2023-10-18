using System.Numerics;

namespace FoxesAndRabbits.Core;

public class Animal
{
    protected int MaxHunger;
    protected int CurrentHunger;
    private readonly int _stepDistance;
    private readonly Grid _grid;
    protected readonly Random Random;
    internal bool IsHungry => CurrentHunger <= MaxHunger / 2 + 1;
    public bool IsBreeding { get; set; }
    public bool IsMoved { get; set; }

    protected Cell? StandingOn;
    
    public Animal(Grid grid, Vector2 startPosition)
    {
        _grid = grid;
        _stepDistance = 1;
        Random = new Random();
        IsBreeding = false;
        IsBreeding = false;
        UpdatePosition(_grid.GetCellAtPosition((int)startPosition.X, (int)startPosition.Y));
    }


    protected void Die()
    {
        Animal currentAnimal = _grid.GetCellAtPosition(StandingOn.XPos, StandingOn.YPos).AnimalStandingOnCell;
        Statistic.NumberOfDeaths++;
        if (currentAnimal is Rabbit) Statistic.NumberOfRabbits--;
        else Statistic.NumberOfFoxes--;

        _grid.GetCellAtPosition(StandingOn.XPos, StandingOn.YPos).AnimalStandingOnCell = null;
    }

    public void DecreaseHunger()
    {
        CurrentHunger--;
        if (CurrentHunger <= 0)
        {
            Die();
        }
    }

    protected void Eat(int foodValue = 3)
    {
        if (CurrentHunger + foodValue <= MaxHunger)
            CurrentHunger += foodValue;
    }

    protected void Eat(int foodValue, Cell grassPosition)
    {
        CurrentHunger += foodValue;
        if (grassPosition.GrassState == Grass.High)
            grassPosition.GrassState = Grass.Medium;
        else if (grassPosition.GrassState == Grass.Medium)
            grassPosition.GrassState = Grass.Low;
    }

    public int GetCurrentHunger()
    {
        return CurrentHunger;
    }

    public void SetCurrentHunger(int hunger)
    {
        this.CurrentHunger = hunger;
    }

protected void Breed(bool isRabbit = true)
    {
        List<Cell> possibleCellsOfTheNewAnimal = GetValidCellsInRange().Where(x => x.AnimalStandingOnCell == null).ToList();
        Cell randomCell = possibleCellsOfTheNewAnimal[Random.Next(0, possibleCellsOfTheNewAnimal.Count)];
        if (isRabbit)
            _grid.Cells[randomCell.XPos, randomCell.YPos].AnimalStandingOnCell = new Rabbit(_grid, new Vector2(randomCell.XPos,randomCell.YPos));
        else
            _grid.Cells[randomCell.XPos, randomCell.YPos].AnimalStandingOnCell = new Fox(_grid, new Vector2(randomCell.XPos,randomCell.YPos));
        _grid.Cells[randomCell.XPos, randomCell.YPos].AnimalStandingOnCell!.IsBreeding = true;
    }

    public void UpdatePosition(Cell nextPosition)
    {
        if (StandingOn != null)
            StandingOn.AnimalStandingOnCell = null;
        nextPosition.AnimalStandingOnCell = this;
        StandingOn = nextPosition;
    }

    public void Step()
    {
        Vector2 nextPosition = NextStep();
        Cell nextPositionCell = _grid.GetCellAtPosition((int)nextPosition.X,(int)nextPosition.Y);
        UpdatePosition(nextPositionCell);
        IsMoved = true;
    }

    protected List<Cell> GetValidCellsInRange()
    {
        List<Cell> validCells = new List<Cell>();
        if (StandingOn == null) return validCells;
        for (int x = StandingOn.XPos - _stepDistance; x <= StandingOn.XPos + _stepDistance; x++)
        {
            for (int y = StandingOn.YPos-_stepDistance; y <= StandingOn.YPos+_stepDistance; y++)
            {
                if (_grid.IsCellValid(x, y) && x != StandingOn.XPos && y != StandingOn.YPos)
                {
                    validCells.Add(_grid.GetCellAtPosition(x,y));
                }
            }
        }
        return validCells;
    }
    protected virtual Vector2 NextStep()
    {
        Vector2 currentPos = new Vector2(StandingOn.XPos, StandingOn.YPos);
        if (IsBreeding)
        {
            return currentPos;
        }
        List<Cell> possibleCells = new List<Cell>();
        if (IsHungry)
        {
            int currentFoodValue =
                Rabbit.CalculateFoodValue(StandingOn.GrassState);
            if (StandingOn.GrassState != Grass.Low && currentFoodValue <= MaxHunger - CurrentHunger)
            {
                Eat(currentFoodValue, StandingOn);
                return currentPos;
            }
            if (GetValidCellsInRange().Exists(x => x is { GrassState: Grass.High, AnimalStandingOnCell: null }))
            {
                possibleCells.AddRange(GetValidCellsInRange().Where(x => x is { GrassState: Grass.High, AnimalStandingOnCell: null }));
            }
            else if (GetValidCellsInRange().Exists(x => x is { GrassState: Grass.Medium, AnimalStandingOnCell: null }))
            {
                possibleCells.AddRange(GetValidCellsInRange().Where(x => x is { GrassState: Grass.Medium, AnimalStandingOnCell: null }));
            }
            else possibleCells.AddRange(GetValidCellsInRange().Where(x=>x.AnimalStandingOnCell == null));
        }
        else if (GetValidCellsInRange().Exists(x => x.AnimalStandingOnCell is Rabbit && !x.AnimalStandingOnCell.IsHungry && !x.AnimalStandingOnCell.IsBreeding)
                 && GetValidCellsInRange().Exists(x => x.AnimalStandingOnCell == null))
        {
            
            var list = GetValidCellsInRange().Where(x =>
                x.AnimalStandingOnCell is Rabbit && !x.AnimalStandingOnCell.IsHungry &&
                !x.AnimalStandingOnCell.IsBreeding);
            var length = GetValidCellsInRange().Count(x =>
                x.AnimalStandingOnCell is Rabbit
                && !x.AnimalStandingOnCell.IsHungry
                && !x.AnimalStandingOnCell.IsBreeding);
            if (length != 0)
            {
                list.ElementAt(Random.Next(length)).AnimalStandingOnCell.IsBreeding = true;
                IsBreeding = true;
                Breed();
                return currentPos;    
            }
            else possibleCells.AddRange(GetValidCellsInRange().Where(x => x.AnimalStandingOnCell == null));
        }
        else possibleCells.AddRange(GetValidCellsInRange().Where(x => x.AnimalStandingOnCell == null));

        if (possibleCells.Count != 0)
        {
            Cell chosenCell = possibleCells[Random.Next(possibleCells.Count)];
            return new Vector2(chosenCell.XPos, chosenCell.YPos);
        }
        
        return currentPos;
    }
}