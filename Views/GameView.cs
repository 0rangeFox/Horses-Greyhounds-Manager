using HaGManager.Helpers.Views;

namespace HaGManager.Views; 

public class GameView : GView {

    public GameView() {
        this.Header = new() {
            $"Day: {Game.Instance.Day}",
            $"Team: {this.Team.Name}"
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
            new("Check my Staffs", () => this.Menu.AddView(new StaffView())),
            new("Staff Market", () => this.Menu.AddView(new StaffMarketView())),
            new("Market", () => this.Menu.AddView(new MarketView())),
            new("Matches", () => this.Menu.AddView(new MatchesView()))
        };

        var trades = Game.Instance.Trades.Count(trade => trade.ToTeam.Equals(this.Team));
        if (trades > 0)
            this.Options.Add(new($"Trade Offers ({trades}x)", () => this.Menu.AddView(new TradesView())));

        return true;
    }

}
