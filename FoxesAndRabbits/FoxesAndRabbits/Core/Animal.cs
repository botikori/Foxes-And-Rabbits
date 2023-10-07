using System.Numerics;

namespace FoxesAndRabbits.Core;

public class Animal
{
    protected int _maxHunger;
    protected int _currentHunger;
    protected int _stepDistance;
    private Grid _grid;
    protected Random random;
    protected bool IsHungry;
    protected bool IsBreeding;
    
    protected Cell? standingOn;

    public static Action? OnStep;

    public Animal(Grid grid, Vector2 startPosition)
    {
        this._grid = grid;
        this._stepDistance = 1;
        this._currentHunger = this._maxHunger;
        random = new Random();
        IsHungry = _currentHunger <= _maxHunger / 2 + 1;
        IsBreeding = false;
        UpdatePosition(_grid.GetCellAtPosition((int)startPosition.X, (int)startPosition.Y));
    }
    

    protected void Die()
    {
        
    }

    public void DecreaseHunger()
    {
        _currentHunger--;
        if (_currentHunger == 0)
        {
            Die();
        }
    }
    
    protected void Eat(int foodValue = 3)
    {
        if (_currentHunger + foodValue <= _maxHunger)
            _currentHunger += foodValue;
    }
    protected void Breed()
    {
        //Ide jön maga a Breed majd, új nyúl teremtése.
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
        for (int x = standingOn.XPos - _stepDistance; x < standingOn.XPos + _stepDistance; x++)
        {
            for (int y = standingOn.YPos-_stepDistance; y < standingOn.YPos+_stepDistance; y++)
            {
                if (_grid.IsCellValid(x, y))
                {
                    validCells.Add(_grid.GetCellAtPosition(x,y));
                }
            }
        }
        return validCells;
    }
    protected virtual Vector2 NextStep()
    {
        if (IsBreeding)
        {
            return new Vector2(standingOn.XPos, standingOn.YPos);
        }
        List<Cell> possibleCells = new List<Cell>();
        if (IsHungry)
        {
            if (GetValidCellsInRange().Exists(x => x.GrassState == Grass.High))
            {
                possibleCells.AddRange(GetValidCellsInRange().Where(x => x is { GrassState: Grass.High, AnimalStandingOnCell: null }));
            }
            else if (GetValidCellsInRange().Exists(x => x is { GrassState: Grass.Medium, AnimalStandingOnCell: null }))
            {
                possibleCells.AddRange(GetValidCellsInRange().Where(x => x is { GrassState: Grass.Medium, AnimalStandingOnCell: null }));
            }
            else possibleCells.AddRange(GetValidCellsInRange().Where(x=>x.AnimalStandingOnCell == null));
        }
        else if (GetValidCellsInRange().Exists(x => x.AnimalStandingOnCell is Rabbit && !x.AnimalStandingOnCell.IsHungry && !x.AnimalStandingOnCell.IsBreeding))
        {
            IsBreeding = true;//Ha van potenciális társa, bekapcsolja az állapotot
            var list = GetValidCellsInRange().Where(x =>
                x.AnimalStandingOnCell is Rabbit && !x.AnimalStandingOnCell.IsHungry &&
                !x.AnimalStandingOnCell.IsBreeding);//potenciális társak listája
            var length = GetValidCellsInRange().Count(x =>
                x.AnimalStandingOnCell is Rabbit
            && !x.AnimalStandingOnCell.IsHungry
            && !x.AnimalStandingOnCell.IsBreeding);//potenciális társak száma
            list.ElementAt(length).AnimalStandingOnCell.IsBreeding = true; //Véletlenszerűen kiválasztott társ állapotának bekapcsolása
            Breed();
        }
        Cell chosenCell = possibleCells[random.Next(possibleCells.Count)];
        return new Vector2(chosenCell.XPos, chosenCell.YPos);
    }
}