using System.Collections.Immutable;
using HaGManager.Extensions;
using HaGManager.Helpers.Views;
using HaGManager.Models;
using HaGManager.Views;
using File = HaGManager.Helpers.File;

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
    public List<Team> Teams { get; }
    private Queue<Team> _shuffledTeamOrderPlay;

    public Game(File.GameFile gameFile) {
        Game.Instance = this;

        this.Time = gameFile.Time;
        this.Teams = gameFile.Teams;
        this._shuffledTeamOrderPlay = gameFile.ShuffledPlayTeam.Count > 0 ? gameFile.ShuffledPlayTeam : this.GetShuffledTeams();

        this.Run();
    }

    private Queue<Team> GetShuffledTeams() => new(this.Teams.Shuffle());

    private void Run() {
        do {
            while (!this._stop && this._shuffledTeamOrderPlay.Count > 0) {
                var team = this._shuffledTeamOrderPlay.Peek();

                new Menu(new GameView(team), "Finish turn", new Dictionary<ConsoleKey, MenuAction?>() {
                    {
                        ConsoleKey.Backspace, (menu) => {
                            this._stop = true;
                            menu.Close();
                        }
                    }
                });

                if (!this._stop)
                    this._shuffledTeamOrderPlay.Dequeue();
            }

            if (this._stop) continue;

            this.Time++;
            this._shuffledTeamOrderPlay = this.GetShuffledTeams();
        } while (!this._stop);

        this.SaveGame();
    }

    private void SaveGame() {
        Console.WriteLine("Saving the game...");
        File.Write(new File.GameFile(this.Time, this.Teams, this._shuffledTeamOrderPlay));
    }

}
