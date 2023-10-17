namespace FoxesAndRabbits.Core;

public class Grid
{
    public int Width { get; set; }
    public int Height { get; set; }
    
    public Cell[,] Cells;

    private void CreateCell(int x, int y)
    {
        Cell currentCell = new Cell(x, y);
        Cells[x, y] = currentCell;
    }

    public Cell GetCellAtPosition(int x, int y)
    {
        if (IsCellValid(x, y))
        {
            return Cells[x, y];
        }
        
        return null;
        
    }

    public bool IsCellValid(int x, int y)
    {
        if (x < 0 || x >= Width || y < 0 || y >= Height)
        {
            return false;
        }

        return true;
    }

    public void CreateGrid(int width, int height)
    {
        Cells = new Cell[width, height];

        Width = width;
        Height = height;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                CreateCell(x, y);
            }
        }
    }
}