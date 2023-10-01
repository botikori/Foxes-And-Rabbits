using FoxesAndRabbits.Core;
using FoxesAndRabbits.UI;

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

char answer;
bool correct;
do
{
    Console.WriteLine("Szeretnéd az állatokat véletlenszerűen elhelyezni? (I/N)");
    correct = char.TryParse(Console.ReadLine().ToLower(), out answer);
} while (!correct);

Grid grid = new Grid(columns,rows);
Simulation simulation = new Simulation(grid,5); //Ezt még tovább kell vinni!

if (answer == 'i')
    simulation.StartSimulation();
else
    Console.WriteLine("Az jó de ezt még nem írtam meg -_-");//Ez még nincs implementálva

DrawGrid drawGrid = new DrawGrid(grid);
drawGrid.Draw();

