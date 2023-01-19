using HaGManager.Helpers.Views;
using HaGManager.Models;

namespace HaGManager.Views; 

public class MarketView : GView {

    public MarketView() {
        this.ReturnMessage = "Return to menu";
    }

    public override bool RefreshView() {
        this.Header = new() {
            $"Day: {Game.Instance.Day}",
            $"Team: {this.Team.Name}",
            $"Balance: {this.Team.Balance}",
            ""
        };

        this.Options.Clear();
        foreach (var seller in Game.Instance.Market)
            this.Options.Add(new ViewOption(
                $"Name: {seller.Animal.Name} | Price: ${(seller.Price > 0 ? $"{(seller.IsSystem ? Game.Instance.Event.GetMarketDiscount(seller.Price) : seller.Price)}" : "Free")} | Seller: {(seller.IsSystem ? $"System {(Game.Instance.Event is Event.MarketDiscount25 or Event.MarketDiscount50 or Event.MarketDiscount75 ? $"| In SALE! ({Game.Instance.Event.GetString().Substring(7)})" : "")}" : seller.Team!.Name)}",
                () => this.Menu.AddView(new MarketInspectView<Horse>(seller as Seller<Horse>)),
                seller.Team != null && seller.Team == this.Team
            ));

        return true;
    }

}
