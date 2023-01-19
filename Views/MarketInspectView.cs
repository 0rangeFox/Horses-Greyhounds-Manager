using HaGManager.Helpers.Views;
using HaGManager.Models;

namespace HaGManager.Views; 

public class MarketInspectView<A> : GView where A: Animal {

    private readonly Seller<A> _seller;

    public MarketInspectView(Seller<A> seller) {
        this._seller = seller;

        this.Header = new() {
            $"Day: {Game.Instance.Day}",
            $"Team: {this.Team.Name}",
            $"Balance: {this.Team.Balance}",
            "",
            "- Horse:",
            $"Name: {this._seller.Animal.Name}",
            $"Speed: {this._seller.Animal.Speed}",
            $"Resistance: {this._seller.Animal.Resistance}",
            $"Weight: {this._seller.Animal.Weight}",
            $"Diseases: {(this._seller.Animal.Diseases.Count > 0 ? string.Join(", ", this._seller.Animal.Diseases) : "Clean")}"
        };

        this.Options.Add(new ViewOption("Buy this horse", this.BuyAnimal, this.Team.Balance < this._seller.Price));
        this.ReturnMessage = "Return to market";
    }

    private void BuyAnimal() {
        this._seller.Animal.BuyAnimal(this.Team);
        this.Menu.RemoveRecentView();
    } 

}
