using System.Numerics;

namespace FoxesAndRabbits;

public class Animal
{
    protected int _maxHunger;
    protected int _currentHunger;
    protected int _stepDistance;
    private Grid _grid;
    
    protected Cell standingOn;


    public Animal(Grid grid)
    {
        this._grid = grid;
        this._stepDistance = 1;
        this._currentHunger = this._maxHunger;
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
    protected void Breed()
    {
        
    }

    public void Step(Vector2 direction)
    {
        Cell nextPosition = _grid.GetCellAtPosition(standingOn.XPos + (int)direction.X * _stepDistance 
            , standingOn.YPos + (int)direction.Y * _stepDistance);
        
    }
}