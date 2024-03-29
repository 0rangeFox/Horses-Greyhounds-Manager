using HaGManager.Helpers.Views;
using HaGManager.Models;
using File = HaGManager.Helpers.File;

namespace HaGManager.Views; 

public class MainView : View {

    private File.SaveGame? _saveGame;

    public override bool RefreshView() {
        this._saveGame = File.Read();

        this.Options = new() {
            new("Start new game", NewGame),
            new($"Load game {(this._saveGame != null ? $"(From last save: {this._saveGame.LastTime})" : "")}", LoadGame, this._saveGame == null),
            new("Credits", Credits)
        };

        return true;
    }

    private void NewGame() {
        var stopCreating = false;
        var newTeams = new List<Team>();

        Console.Clear();
        do {
            Console.WriteLine("Name of Team:");
            var name = $"{Console.ReadLine()}";
            Console.WriteLine("First Horse name:");
            var horseName = $"{Console.ReadLine()}";
            Console.WriteLine("First Greyhound name:");
            var greyhoundName = $"{Console.ReadLine()}";
            
            newTeams.Add(new Team(name, horseName, greyhoundName));
            
            Console.WriteLine("Create another team? (Press Y to create or others key to cancel)");
            if (Console.ReadKey().Key != ConsoleKey.Y)
                stopCreating = true;
            Console.Clear();
        } while (!stopCreating);

        new Game(new File.GameFile(0, Event.None, newTeams.AsReadOnly()));
    }

    private void LoadGame() {
        new Game(this._saveGame!.File);
    }

    private void Credits() {
        Console.WriteLine("Horses and Greyhound Manager - Game");
        Console.WriteLine("Project was developed for the class OOP.");
        Console.WriteLine("");
        Console.WriteLine("Developers:");
        Console.WriteLine("16621 - John Fernandes");
        Console.WriteLine("23479 - Nuno Ribeiro");
        Console.WriteLine("23490 - Tiago Mendes");
        Console.WriteLine("26317 - Muhammad Nabeel");
        Console.WriteLine("");
        Console.WriteLine("Press any key to close.");

        Console.ReadKey();
    }

}
