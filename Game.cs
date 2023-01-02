using HaGManager.Extensions;
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

    public int Time { get; } = 0;
    public List<Team> Teams { get; } = new();

    public Game() {
        Game.Instance = this;

        this.LoadGame();

        do {
            foreach (var team in this.Teams.Shuffle()) {
                new Menu(new GameView(team), "Finish turn", new Dictionary<ConsoleKey, MenuAction?>() {
                    {
                        ConsoleKey.Backspace, (menu) => {
                            this._stop = true;
                            menu.Close();
                        }
                    }
                });
            }

            this.Time++;
        } while (!this._stop);

        this.SaveGame();
    }

    private void LoadGame() {
        Console.WriteLine("Loading the game...");
    }

    private void SaveGame() {
        Console.WriteLine("Saving the game...");
    }

}