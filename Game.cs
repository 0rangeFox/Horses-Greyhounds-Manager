using System.Collections.ObjectModel;
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
    public ReadOnlyCollection<Team> Teams { get; }
    private Queue<Team> _shuffledTeamOrderPlay;
    public Team ActualTeamPlaying => this._shuffledTeamOrderPlay.Peek();

    public List<Match> Matches { get; }

    public Game(File.GameFile gameFile) {
        Game.Instance = this;

        this.Time = gameFile.Time;
        this.Teams = gameFile.Teams;
        this._shuffledTeamOrderPlay = gameFile.ShuffledPlayTeam.Count > 0 ? gameFile.ShuffledPlayTeam : this.GetShuffledTeams();
        this.Matches = gameFile.Matches.Count > 0 ? gameFile.Matches : this.GenerateMatches();

        this.Run();
    }

    private Queue<Team> GetShuffledTeams() => new(this.Teams.Shuffle());

    private bool ShouldAddNewMatch(int index, int maxAmount) {
        var rng1 = RandomExtension.Random.Next(0, maxAmount + this.Time);
        var rng2 = (index * rng1) * this.Time;
        return rng1 > rng2;
    }

    private List<Match> GenerateMatches() {
        var newMatches = new List<Match>();

        var horsesAmount = this.Teams.Sum(team => team.Horses.Count);
        var greyhoundAmount = this.Teams.Count;

        for (int i = 0; i < horsesAmount; i++)
            if (this.ShouldAddNewMatch(i, horsesAmount))
                newMatches.Add(new(this.Time));

        for (int i = 0; i < greyhoundAmount; i++)
            if (this.ShouldAddNewMatch(i, greyhoundAmount))
                newMatches.Add(new(this.Time));

        return newMatches;
    }

    private void Run() {
        do {
            while (!this._stop && this._shuffledTeamOrderPlay.Count > 0) {
                new Menu(new GameView(), new Dictionary<ConsoleKey, MenuAction?>() {
                    {
                        ConsoleKey.Backspace, (menu) => {
                            if (menu.Views.Count() > 1) {
                                menu.RemoveRecentView();
                                return;
                            }

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
        File.Write(new File.GameFile(this.Time, this.Teams, this._shuffledTeamOrderPlay, this.Matches));
    }

}
