using HaGManager.Helpers.Views;
using HaGManager.Models;

namespace HaGManager.Views; 

public class TeamHorseInspectView : View {

    private readonly Team _team = Game.Instance.ActualTeamPlaying;
    private readonly Horse _horse;

    public TeamHorseInspectView(Horse horse) {
        this._horse = horse;

        this.ReturnMessage = "Return to horses garage";
    }

    public override bool RefreshView() {
        if (!this._team.Horses.Contains(this._horse)) {
            this.Menu.RemoveRecentView();
            return false;
        }

        this.Header = new() {
            $"Day: {Game.Instance.Day}",
            $"Team: {this._team.Name}",
            $"Balance: {this._team.Balance}",
            "",
            $"- Inspecting the horse: {this._horse.Name}",
            $"Energy: {this._horse.Energy}",
            $"Speed: {this._horse.Speed}",
            $"Resistance: {this._horse.Resistance}",
            $"Weight: {this._horse.Weight}",
            $"Diseases: {(this._horse.Diseases.Count > 0 ? string.Join(", ", this._horse.Diseases) : "Clean")}",
            $"Status: {this._horse.Status}",
            ""
        };

        this.Options.Clear();
        if (this._horse.IsInMarket)
            this.Options.Add(new("Remove from market", this.RemoveFromMarket));
        else {
            this.Options.Add(new("Sell", () => this.Menu.AddView(new SellMenu(this._horse))));
            this.Options.Add(new("Trade", null, true));
        }

        return true;
    }

    private void RemoveFromMarket() => this._horse.BuyAnimal(this._team);

    private class SellMenu : View {

        private readonly Horse _horse;

        public SellMenu(Horse horse) {
            this._horse = horse;

            this.Options = new() {
                new($"Quick sell for ${this._horse.QuickSellPrice}", this.QuickSell),
                new("Sell to market", this.SellToMarket)
            };

            this.ReturnMessage = "Cancel";
        }

        private void QuickSell() {
            this._horse.SellAnimal(0, true);
            this.Menu.RemoveRecentView();
        }

        private void SellToMarket() {
            float price = -1f;
            do {
                Console.Clear();
                Console.Write("Price for the horse: ");
                float.TryParse(Console.ReadLine(), out price);
            } while (price < 0);
            this._horse.SellAnimal(price);
            this.Menu.RemoveRecentView();
        }

    }

}
