using HaGManager.Helpers.Views;
using HaGManager.Models;

namespace HaGManager.Views; 

public class GameView : GView {

    public GameView() {
        this.Header = new() {
            $"Day: {Game.Instance.Day} {(Game.Instance.Event != Event.None ? $"(Event: {Game.Instance.Event.GetString()})" : "")}",
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
            new($"Staffs {(Game.Instance.Event == Event.StaffStrike ? "(Closed due to Staff Strike)" : "")}", () => this.Menu.AddView(new StaffView())),
            new("Staff Market", () => this.Menu.AddView(new StaffMarketView())),
            new($"Market {(Game.Instance.Event == Event.MarketStrike ? "(Closed due to Market Strike)" : "")}", () => this.Menu.AddView(new MarketView()), Game.Instance.Event == Event.MarketStrike),
            new("Matches", () => this.Menu.AddView(new MatchesView()))
        };

        var trades = Game.Instance.Trades.Count(trade => trade.ToTeam.Equals(this.Team));
        if (trades > 0)
            this.Options.Add(new($"Trade Offers ({trades}x)", () => this.Menu.AddView(new TradesView())));

        return true;
    }

}
