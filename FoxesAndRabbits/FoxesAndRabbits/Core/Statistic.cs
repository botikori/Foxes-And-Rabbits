namespace FoxesAndRabbits.Core;

public static class Statistic
{
    public static int numberOfFoxes { get; set;}
    public static int numberOfRabbits { get; set;}
    public static int numberOfDeaths { get; set; }
    public static int numberOfRounds { get; set; }

    public static void ShowStatistic()
    {
        Console.WriteLine("\nA játék véget ért!");
        Console.WriteLine("\nStatisztika\n");
        Console.WriteLine($"Eltelt körök száma: {numberOfRounds}\nRókák száma: {numberOfFoxes}\nNyulak száma: {numberOfRabbits}\nJáték során keletkezett halálesetek száma: {numberOfDeaths}");
    }
}