using System.Numerics;

namespace FoxesAndRabbits.Core;

public class Animal
{
    protected int _maxHunger;
    protected int _currentHunger;
    protected int _stepDistance;
    private Grid _grid;
    protected Random random;
    internal bool IsHungry => _currentHunger <= _maxHunger / 2 + 1;
    public bool IsBreeding;

    protected Cell? standingOn;

    public static Action? OnStep;

    public Animal(Grid grid, Vector2 startPosition)
    {
        this._grid = grid;
        this._stepDistance = 1;
        random = new Random();
        IsBreeding = false;
        UpdatePosition(_grid.GetCellAtPosition((int)startPosition.X, (int)startPosition.Y));
    }


    protected void Die()
    {
        Animal currentAnimal = _grid.GetCellAtPosition(standingOn.XPos, standingOn.YPos).AnimalStandingOnCell;
        if (currentAnimal is Rabbit) Statistic.numberOfRabbits--;
        else Statistic.numberOfFoxes--;

        _grid.GetCellAtPosition(standingOn.XPos, standingOn.YPos).AnimalStandingOnCell = null;
        Statistic.numberOfDeaths++;
    }

    public void DecreaseHunger()
    {
        _currentHunger--;
        if (_currentHunger <= 0)
        {
            Die();
        }
    }

    protected void Eat(int foodValue = 3)
    {
        if (_currentHunger + foodValue <= _maxHunger)
            _currentHunger += foodValue;
    }

    protected void Eat(int foodValue, Cell grassPosition)
    {
        _currentHunger += foodValue;
        if (grassPosition.GrassState == Grass.High)
            grassPosition.GrassState = Grass.Medium;
        else if (grassPosition.GrassState == Grass.Medium)
            grassPosition.GrassState = Grass.Low;
    }

    public int GetCurrentHunger()
    {
        return _currentHunger;
    }

    public void SetCurrentHunger(int hunger)
    {
        this._currentHunger = hunger;
    }

protected void Breed()
    {
        List<Cell> possibleCellsOfTheNewAnimal = GetValidCellsInRange().Where(x => x.AnimalStandingOnCell == null).ToList();
        Cell randomCell = possibleCellsOfTheNewAnimal[random.Next(0, possibleCellsOfTheNewAnimal.Count)];
        _grid.Cells[randomCell.XPos, randomCell.YPos].AnimalStandingOnCell = new Rabbit(_grid, new Vector2(randomCell.XPos,randomCell.YPos));
        _grid.Cells[randomCell.XPos, randomCell.YPos].AnimalStandingOnCell!.IsBreeding = true;//Kör végén kikapcsolni!!!
    }

    public void UpdatePosition(Cell nextPosition)
    {
        if (standingOn != null)
            standingOn.AnimalStandingOnCell = null;
        nextPosition.AnimalStandingOnCell = this;
        standingOn = nextPosition;
    }
    public void Step(Vector2 direction)
    {
        Cell nextPosition = _grid.GetCellAtPosition(standingOn.XPos + (int)direction.X * _stepDistance 
            , standingOn.YPos + (int)direction.Y * _stepDistance);
        UpdatePosition(nextPosition);
        OnStep?.Invoke();
    }
    public void Step()
    {
        Vector2 nextPosition = NextStep();
        Cell nextPositionCell = _grid.GetCellAtPosition((int)nextPosition.X,(int)nextPosition.Y);
        UpdatePosition(nextPositionCell);
        //OnStep?.Invoke();
    }

    protected List<Cell> GetValidCellsInRange()
    {
        List<Cell> validCells = new List<Cell>();
        if (standingOn == null) return validCells;
        for (int x = standingOn.XPos - _stepDistance; x <= standingOn.XPos + _stepDistance; x++)
        {
            for (int y = standingOn.YPos-_stepDistance; y <= standingOn.YPos+_stepDistance; y++)
            {
                if (_grid.IsCellValid(x, y) && x != standingOn.XPos && y != standingOn.YPos)
                {
                    validCells.Add(_grid.GetCellAtPosition(x,y));
                }
            }
        }
        return validCells;
    }
    protected virtual Vector2 NextStep()
    {
        Vector2 currentPos = new Vector2(standingOn.XPos, standingOn.YPos);
        if (IsBreeding)
        {
            return currentPos;
        }
        List<Cell> possibleCells = new List<Cell>();
        if (IsHungry)
        {
            int currentFoodValue =
                Rabbit.CalculateFoodValue(standingOn.GrassState);
            if (standingOn.GrassState != Grass.Low && currentFoodValue <= _maxHunger - _currentHunger)
            {
                Eat(currentFoodValue, standingOn);
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
                !x.AnimalStandingOnCell.IsBreeding);//potenciális társak listája
            var length = GetValidCellsInRange().Count(x =>
                x.AnimalStandingOnCell is Rabbit
                && !x.AnimalStandingOnCell.IsHungry
                && !x.AnimalStandingOnCell.IsBreeding);//potenciális társak száma
            if (length != 0)
            {
                list.ElementAt(random.Next(length)).AnimalStandingOnCell.IsBreeding = true;
                IsBreeding = true;
                Breed();
                return currentPos;    
            }
            else possibleCells.AddRange(GetValidCellsInRange().Where(x => x.AnimalStandingOnCell == null));
        }
        else possibleCells.AddRange(GetValidCellsInRange().Where(x => x.AnimalStandingOnCell == null));

        if (possibleCells.Count != 0)
        {
            Cell chosenCell = possibleCells[random.Next(possibleCells.Count)];
            return new Vector2(chosenCell.XPos, chosenCell.YPos);
        }

        return currentPos;
    }
}