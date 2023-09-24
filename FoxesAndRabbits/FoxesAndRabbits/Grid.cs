namespace FoxesAndRabbits;

public class Grid
{
    private int _width, _height;
    private Cell[,] _cells;

    public void CreateBoard(int width, int height)
    {
        _cells = new Cell[width, height];

        this._width = width;
        this._height = height;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                CreateCell(x, y);
            }
        }
    }

    private void CreateCell(int x, int y)
    {
        Cell currentCell = new Cell(x, y);
        _cells[x, y] = currentCell;
    }

    public Cell GetCellAtPosition(int x, int y)
    {
        if (IsCellValid(x, y))
        {
            return _cells[x, y];
        }
        
        return null;
        
    }

    public bool IsCellValid(int x, int y)
    {
        if (x < 0 || x >= _width || y < 0 || y >= _height)
        {
            return false;
        }

        return true;
    }
}