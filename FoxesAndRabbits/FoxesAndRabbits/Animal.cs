using System.Numerics;

namespace FoxesAndRabbits;

public class Animal
{
    protected int _hunger;
    protected int _stepDistance;
    private Grid _grid;
    
    protected Cell standingOn;


    public Animal(Grid grid)
    {
        this._grid = grid;
    }
    

    protected void Die()
    {
        
    }

    public void DecreaseHunger()
    {
        _hunger--;
        if (_hunger == 0)
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