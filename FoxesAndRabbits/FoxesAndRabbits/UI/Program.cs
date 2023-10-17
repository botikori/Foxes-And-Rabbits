using System.Numerics;
using FoxesAndRabbits.Core;
using FoxesAndRabbits.UI;

string loadAnswer;
if (File.Exists("game.txt"))
{
    do
    {
        Console.WriteLine("Mentési fájl észlelve!");
        Console.Write("Szeretnéd betölteni a játékot (i/n):");
        loadAnswer = Console.ReadLine();
        Console.Clear();
        
    } while (!(loadAnswer.ToLower() == "i" || loadAnswer.ToLower() == "n"));

    if (loadAnswer == "n")SetUpGame();
    else
    {
        
    }
}


SetUpGame();
    
void SetUpGame()
{
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


    int numberOfFoxes;
    int numberOfRabbits;
    int numberOfAnimals;
    bool correctAmount;
    do
    {
        Console.WriteLine($"#3 Állatok száma a pályán");
        Console.Write("Nyúlak száma: ");
        correctAmount = int.TryParse(Console.ReadLine(), out numberOfRabbits);
        Console.Write("Rókák száma: ");
        correctAmount = int.TryParse(Console.ReadLine(), out numberOfFoxes);
        numberOfAnimals = numberOfFoxes + numberOfRabbits;
        if (numberOfAnimals > (rows*columns)*0.3)
        {
            correctAmount = false;
        }
        Console.Clear();
    } while (!correctAmount);

    char answer;
    bool correct;
    do
    {
        Console.WriteLine("#4 Szeretnéd az állatokat véletlenszerűen elhelyezni? (I/N)");
        correct = char.TryParse(Console.ReadLine().ToLower(), out answer);
        if (answer != 'i' && answer != 'n')
        {
            correct = false;
            Console.WriteLine("Hibás válasz!");
        }
        
    } while (!correct);

    Grid grid = new Grid();
    grid.CreateGrid(columns,rows);
    
    Simulation simulation = new Simulation(grid,numberOfRabbits,numberOfFoxes);

    if (answer == 'i')
        simulation.StartSimulation();
    else if (answer == 'n')
    {
        List<Vector2> positionsOfFoxes = new List<Vector2>();
        List<Vector2> positionsOfRabbits = new List<Vector2>();
        string[] pos;

        for (int i = 0; i < numberOfRabbits; i++)
        {
            do
            {
                Console.Write($"{i + 1}. nyúl helye(x,y): ");
                pos = Console.ReadLine().Split(',');
                
            } while (!simulation.IsGivenPositionCorrect(pos) || positionsOfRabbits.Exists(x=>x.X == int.Parse(pos[0]) 
                     && x.Y == int.Parse(pos[1])));
            positionsOfRabbits.Add(new Vector2(int.Parse(pos[0]), int.Parse(pos[1])));
            
        }
        
        for (int i = 0; i < numberOfFoxes; i++)
        {
            do
            {
                Console.Write($"{i + 1}. róka helye(x,y): ");
                pos = Console.ReadLine().Split(',');
                
            } while (!simulation.IsGivenPositionCorrect(pos) || positionsOfRabbits.Exists(x=>x.X == int.Parse(pos[0]) 
                         && x.Y == int.Parse(pos[1]) || 
                         positionsOfFoxes.Exists(x=>x.X == int.Parse(pos[0]) && x.Y == int.Parse(pos[1]))));
            positionsOfFoxes.Add(new Vector2(int.Parse(pos[0]), int.Parse(pos[1])));
        }
        simulation.StartSimulation(positionsOfRabbits,positionsOfFoxes);
    }
    Console.Clear();
    DrawGrid drawGrid = new DrawGrid(grid);
    drawGrid.Draw();

    while (!simulation.EndOfSimulation())
    {
        Console.WriteLine($"Rókák: {Statistic.numberOfFoxes} \t Nyulak: {Statistic.numberOfRabbits}");
        Console.Write($"Szeretnél a következő képkockára lépni vagy menteni? (I/mentes):");
        
        string answer2 = Console.ReadLine();

        if (answer2.ToLower() == "i")
        {
            simulation.Tick();
            drawGrid.UpdateGrid();
        }
        else if (answer2.ToLower() == "mentes")
        {
            Console.Clear();
            simulation.SaveGame();
            Console.WriteLine("Sikeres mentés!\nA játék mentésre került 'game.txt' néven!");
            break;
        }
        simulation.EndOfSimulation();
    }

    ShowStatistic();
}

void ShowStatistic()
{
    Console.WriteLine("\nA játék véget ért!");
    Console.WriteLine("\nStatisztika\n");
    Console.WriteLine($"Eltelt körök száma: {Statistic.numberOfRounds}\nRókák száma: {Statistic.numberOfFoxes}\nNyulak száma: {Statistic.numberOfRabbits}\nJáték során keletkezett halálesetek száma: {Statistic.numberOfDeaths}");
}

