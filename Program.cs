using HaGManager.Models;

namespace HaGManager;

internal class Program {

    public static bool Stop = false;
    public static int Time = 0;
    public static List<Team> Teams = new List<Team>();

    static void Main(string[] args) {
        LoadGame();

        do {
            Console.WriteLine($"Day: {Time}");
            if (Console.ReadLine().Equals("stop"))
                Stop = true;

            Time++;
        } while (!Stop);

        SaveGame();
    }

    private static void LoadGame() {
        Console.WriteLine("Loading the game...");
    }
    
    private static void SaveGame() {
        Console.WriteLine("Saving the game...");
    }

}
