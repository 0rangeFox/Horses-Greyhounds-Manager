using HaGManager.Helpers.Views;
using HaGManager.Models;

namespace HaGManager.Views; 

public class GameView : View {

    private readonly Team _team = Game.Instance.ActualTeamPlaying;

    public GameView() {
        this.Header = new() {
            $"Day: {Game.Instance.Day}",
            $"Team: {this._team.Name}"
        };

        this.Footer = new() {
            "",
            $"Click on the backspace to go to the main menu."
        };

        this.ReturnMessage = "Finish turn";
    }

    public override bool RefreshView() {
        this.Options = new() {
            new("Check my horses", () => this.Menu.AddView(new TeamHorsesView())),
            new("Level Up", LevelUp),
            new("Market", () => this.Menu.AddView(new MarketView())),
            new("Matches", () => this.Menu.AddView(new MatchesView()))
        };

        var trades = Game.Instance.Trades.Count(trade => trade.ToTeam.Equals(this._team));
        if (trades > 0)
            this.Options.Add(new($"Trade Offers ({trades}x)", () => this.Menu.AddView(new TradesView())));

        return true;
    }

    private void LevelUp() {
        this._team.AddExperience(1000);
        Console.WriteLine($"Balance: {this._team.Balance}");
        this._team.AddExperience(2000);
        Console.WriteLine($"Balance: {this._team.Balance}");
        this._team.AddExperience(5000);
        Console.WriteLine($"Balance: {this._team.Balance}");
        Console.ReadKey();
    }

}
