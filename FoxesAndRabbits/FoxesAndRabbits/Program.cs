using FoxesAndRabbits;

int columns;
int rows;

do
{
    Console.Write("#1 Oszlopok száma(10-20): ");
    
    bool succes = int.TryParse(Console.ReadLine(), out columns);

    if (!succes) Console.WriteLine("Számot kell megadni!");
    else
    {
        if (columns < 10) Console.WriteLine("10-nél nagyobb számot kell megadni!");
        if (columns > 20) Console.WriteLine("20-nál kisebb számot kell megadni!");
    }
} while (columns<10 || columns > 20);

do
{
    Console.Write("#2 Sorok száma(10-20): ");
    
    bool succes = int.TryParse(Console.ReadLine(), out rows);

    if (!succes) Console.WriteLine("Számot kell megadni!");
    else
    {
        if (rows < 10) Console.WriteLine("10-nél nagyobb számot kell megadni!");
        if (rows > 20) Console.WriteLine("20-nál kisebb számot kell megadni!");
    }

} while (rows<10 || rows > 20);


Console.Clear();



Grid grid = new Grid(columns,rows);
grid.DrawGrid();