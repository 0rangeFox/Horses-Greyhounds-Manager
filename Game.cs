using HaGManager.Helpers.Views;
using HaGManager.Models;
using HaGManager.Views;

namespace HaGManager; 

public class Game {

    public static Game Instance = null!;

    private bool _stop = false;

    public bool Stop {
        get => this._stop;
        set {
            if (this._stop) return;
            this._stop = value;
        }
    }

    public int Time { get; private set; } = 0;
    public List<Team> Teams { get; } = new();

    public Game() {
        Game.Instance = this;

        do {
            new Menu();
            this.Time++;
        } while (!this._stop);

        this.SaveGame();
    }

    public void LoadGame() {
        Console.WriteLine("Loading the game...");
        Console.ReadKey();
    }

    private void SaveGame() {
        Console.WriteLine("Saving the game...");
    }

}
