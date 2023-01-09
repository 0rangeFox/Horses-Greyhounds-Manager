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

    public int Day { get; private set; } = 0;
    public ReadOnlyCollection<Team> Teams { get; }
    private Queue<Team> _shuffledTeamOrderPlay;
    public Team ActualTeamPlaying => this._shuffledTeamOrderPlay.Peek();

    public List<IMatch<Animal>> Matches { get; private set; }

    public Game(File.GameFile gameFile) {
        Game.Instance = this;

        this.Day = gameFile.Day;
        this.Teams = gameFile.Teams;
        this._shuffledTeamOrderPlay = gameFile.ShuffledPlayTeam.Count > 0 ? gameFile.ShuffledPlayTeam : this.GetShuffledTeams();
        this.Matches = gameFile.Matches.Count > 0 ? gameFile.Matches : this.GenerateMatches();

        this.Run();
    }

    private Queue<Team> GetShuffledTeams() => new(this.Teams.Shuffle());

    private bool ShouldAddNewMatch(int index, int maxAmount) {
        var rng1 = RandomExtension.Random.Next(0, maxAmount + this.Day);
        var rng2 = (index * rng1) * this.Day;
        return rng1 > rng2;
    }

    private List<IMatch<Animal>> GenerateMatches(List<IMatch<Animal>>? matches = null) {
        var newMatches = new List<IMatch<Animal>>(matches ?? new List<IMatch<Animal>>());

        var horsesMatches = newMatches.Count(match => match is Match<Horse>);
        var greyhoundsMatches = newMatches.Count(match => match is Match<Greyhound>);

        var horsesAmount = this.Teams.Sum(team => team.Horses.Count);
        var greyhoundAmount = this.Teams.Count;

        if (horsesMatches <= horsesAmount)
            for (int i = 0; i < RandomExtension.Random.Next(3); i++)
                if (this.ShouldAddNewMatch(i, horsesAmount))
                    newMatches.Add(new Match<Horse>());

        if (greyhoundsMatches <= greyhoundAmount)
            for (int i = 0; i < RandomExtension.Random.Next(3); i++)
                if (this.ShouldAddNewMatch(i, greyhoundAmount))
                    newMatches.Add(new Match<Greyhound>());

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

            this.SkipDay();
        } while (!this._stop);

        this.SaveGame();
    }

    private void SkipDay() {
        for (int i = this.Matches.Count - 1; i >= 0; i--)
            this.Matches[i].Start();

        this.Day++;
        this._shuffledTeamOrderPlay = this.GetShuffledTeams();
        this.Matches = this.GenerateMatches(this.Matches);
    }

    private void SaveGame() {
        Console.WriteLine("Saving the game...");
        File.Write(new File.GameFile(this.Day, this.Teams, this._shuffledTeamOrderPlay, this.Matches));
    }

}
