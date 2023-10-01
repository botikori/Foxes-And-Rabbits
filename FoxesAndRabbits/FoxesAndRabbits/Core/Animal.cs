using System.Numerics;

namespace FoxesAndRabbits.Core;

public class Animal
{
    protected int _maxHunger;
    protected int _currentHunger;
    protected int _stepDistance;
    private Grid _grid;
    
    protected Cell? standingOn;

    public static Action? OnStep;

    public Animal(Grid grid, Vector2 startPosition)
    {
        this._grid = grid;
        this._stepDistance = 1;
        this._currentHunger = this._maxHunger;
        
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
        
    }

    public void UpdatePosition(Cell nextPosition)
    {
        if (standingOn != null)
            standingOn.AnimalStandingOnCell = null;
        nextPosition.AnimalStandingOnCell = this;
        standingOn = nextPosition;
    }

    private bool CanStep(Vector2 direction)
    {
        if (_grid.IsCellValid(standingOn.XPos + (int)direction.X * _stepDistance 
                , standingOn.YPos + (int)direction.Y * _stepDistance))
        {
            return true;
        }

        return false;
    }
    
    public void Step(Vector2 direction)
    {
        Cell nextPosition = _grid.GetCellAtPosition(standingOn.XPos + (int)direction.X * _stepDistance 
            , standingOn.YPos + (int)direction.Y * _stepDistance);
        UpdatePosition(nextPosition);
        OnStep?.Invoke();
    }
}