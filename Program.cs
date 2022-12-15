using HaGManager.Helpers.Menus;
using HaGManager.Models;

namespace HaGManager;

internal class Program {

    public static bool Stop = false;
    public static int Time = 0;
    public static List<Team> Teams = new List<Team>();

    private static List<Option> menus = new List<Option> {
        new Option("Start new game"),
        new Option("Load game"),
        new Option("Credits"),
        new Option("Exit", () => {
            Stop = !Stop;
        })
    };

    static void Main(string[] args) {
        LoadGame();

        // do {
        //     Console.WriteLine($"Day: {Time}");
        //
        //     Time++;
        // } while (!Stop);

        var newTeam = new Team("Equipa N1", "Manel", "Joaquim");
        Console.WriteLine($"Team: {newTeam.Name} has x{newTeam.GetFreeHorsesSlots()} free slots.");

        Console.WriteLine($"Balance: {newTeam.Balance}");
        newTeam.AddExperience(5000);
        Console.WriteLine($"Balance: {newTeam.Balance}");
        newTeam.AddExperience(5000);
        Console.WriteLine($"Balance: {newTeam.Balance}");

        SaveGame();
    }

    private static void LoadGame() {
        Console.WriteLine("Loading the game...");
    }
    
    private static void SaveGame() {
        Console.WriteLine("Saving the game...");
    }

}
