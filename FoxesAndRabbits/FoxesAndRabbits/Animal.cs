using System.Numerics;

namespace FoxesAndRabbits;

public class Animal
{
    protected int _hunger;
    protected int _stepDistance;

    protected Cell standingOn;


    public Animal()
    {
        
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
        
    }

    
}