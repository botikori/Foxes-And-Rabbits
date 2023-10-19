using System.Numerics;

namespace FoxesAndRabbits.Core;

public class Fox : Animal
{
    public Fox(Grid grid, Vector2 startPosition) : base(grid, startPosition)
    {
        MaxHunger = 6;
        CurrentHunger = MaxHunger;
        Statistic.NumberOfFoxes++;
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
            Statistic.NumberOfDeaths++;
            Statistic.NumberOfRabbits--;
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
                Breed(false);
                return new Vector2(StandingOn.XPos, StandingOn.YPos);    
            }

            possibleCells.AddRange(GetValidCellsInRange().Where(x => x.AnimalStandingOnCell == null));
        }
        else possibleCells.AddRange(GetValidCellsInRange().Where(x => x.AnimalStandingOnCell == null));

        if (possibleCells.Count != 0)
        {
            Cell chosenCell = possibleCells[Random.Next(possibleCells.Count)];
            if (chosenCell.AnimalStandingOnCell != null) chosenCell.AnimalStandingOnCell = null;
            return new Vector2(chosenCell.XPos, chosenCell.YPos);
        }

        return new Vector2(StandingOn.XPos, StandingOn.YPos);
    }
}