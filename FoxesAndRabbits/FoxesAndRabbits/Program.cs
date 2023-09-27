using FoxesAndRabbits;

int columns;
int rows;

do
{
    Console.Write("#1 Oszlopok száma(10-20): ");
    columns = int.Parse(Console.ReadLine());
} while (columns<10 || columns > 20);
do
{
    Console.Write("#2 Sorok száma(10-20): ");
    rows= int.Parse(Console.ReadLine());
} while (rows<10 || rows > 20);


Console.Clear();



Grid grid = new Grid(columns,rows);
grid.DrawGrid();