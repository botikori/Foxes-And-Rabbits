using System.Numerics;
using FoxesAndRabbits.Core.Exceptions;

namespace FoxesAndRabbits.Core;

public class Fox : Animal
{
    private const int MinDetectRange = 2;
    private const int MaxDetectRange = 6;
    
    public static void SetFoxDetectRange(int range)
    {
        if (range > MaxDetectRange || range < MinDetectRange) 
        {
            throw new ArgumentOutOfRangeException("Fox detection range must be between 2 and 6!");
        }

        if (Rabbit.DetectRange >= range)
        {
            throw new FoxRangeTooLowException();
        }
    }

    public Fox(Grid grid, Vector2 startPosition) : base(grid, startPosition)
    {
        MaxHunger = 10;
        CurrentHunger = MaxHunger;
        Statistic.numberOfFoxes++;
    }
    
    protected override Vector2 NextStep()
    {
        if (IsBreeding)
        {
            return new Vector2(StandingOn.XPos, StandingOn.YPos);
        }
        List<Cell> possibleCells = new List<Cell>();
        if (IsHungry && GetValidCellsInRange().Exists(x => x.AnimalStandingOnCell is Rabbit))
        {
            possibleCells.AddRange(GetValidCellsInRange().Where(x=>x.AnimalStandingOnCell is Rabbit));
            Eat();
            Statistic.numberOfRabbits--;
        }
        else if (GetValidCellsInRange().Exists(x => x.AnimalStandingOnCell is Fox && !x.AnimalStandingOnCell.IsHungry && !x.AnimalStandingOnCell.IsBreeding)
                 && GetValidCellsInRange().Exists(x => x.AnimalStandingOnCell == null))
        {
            var list = GetValidCellsInRange().Where(x =>
                x.AnimalStandingOnCell is Fox && !x.AnimalStandingOnCell.IsHungry &&
                !x.AnimalStandingOnCell.IsBreeding);
            var length = GetValidCellsInRange().Count(x =>
                x.AnimalStandingOnCell is Fox
                && !x.AnimalStandingOnCell.IsHungry
                && !x.AnimalStandingOnCell.IsBreeding);
            if (length != 0)
            {
                list.ElementAt(Random.Next(length)).AnimalStandingOnCell.IsBreeding = true;
                IsBreeding = true;
                Breed();
                return new Vector2(StandingOn.XPos, StandingOn.YPos);    
            }

            possibleCells.AddRange(GetValidCellsInRange().Where(x => x.AnimalStandingOnCell == null));
        }
        else possibleCells.AddRange(GetValidCellsInRange().Where(x => x.AnimalStandingOnCell == null));

        if (possibleCells.Count != 0)
        {
            Cell chosenCell = possibleCells[Random.Next(possibleCells.Count)];
            return new Vector2(chosenCell.XPos, chosenCell.YPos);
        }

        return new Vector2(StandingOn.XPos, StandingOn.YPos);
    }
}