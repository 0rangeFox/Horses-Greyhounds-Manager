using HaGManager.Helpers.Views;
using HaGManager.Models;

namespace HaGManager.Views; 

public class MarketView : View {

    private readonly Team _team = Game.Instance.ActualTeamPlaying;

    public MarketView() {
        this.ReturnMessage = "Return to menu";
    }

    public override bool RefreshView() {
        this.Header = new() {
            $"Day: {Game.Instance.Day}",
            $"Team: {this._team.Name}",
            $"Balance: {this._team.Balance}",
            ""
        };

        this.Options.Clear();
        foreach (var seller in Game.Instance.Market)
            this.Options.Add(new ViewOption(
                $"Name: {seller.Animal.Name} | Price: ${(seller.Price > 0 ? $"{seller.Price}" : "Free")} | Seller: {(seller.Team != null ? seller.Team.Name : "System")}",
                () => this.Menu.AddView(new MarketInspectView<Horse>(seller as Seller<Horse>)),
                seller.Team != null && seller.Team == this._team
            ));

        return true;
    }

}
