using HaGManager.Helpers.Views;
using HaGManager.Models;

namespace HaGManager.Views; 

public class TeamHorseInspectView : GView {

    private readonly Horse _horse;

    public TeamHorseInspectView(Horse horse) {
        this._horse = horse;

        this.ReturnMessage = "Return to horses garage";
    }

    public override bool RefreshView() {
        if (!this.Team.Horses.Contains(this._horse)) {
            this.Menu.RemoveRecentView();
            return false;
        }

        this.Header = new() {
            $"Day: {Game.Instance.Day}",
            $"Team: {this.Team.Name}",
            $"Balance: {this.Team.Balance}",
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
        if (this._horse.IsInMarket) {
            var seller = this._horse.GetSeller<Horse>()!;
            this.Options.Add(new($"Remove from market | Selling on market for ${seller.Price}", this.RemoveFromMarket));
        } else
            this.Options.Add(new("Sell", () => this.Menu.AddView(new SellMenu(this._horse)), this._horse.IsInRace || this._horse.IsInTrade));

        if (this._horse.IsInTrade) {
            var trade = this._horse.GetTrade<Horse>()!;
            this.Options.Add(new($"Remove from trade | Trading for the horse \"{trade.ToAnimal.Name}\" from team \"{trade.ToTeam.Name}\"{(trade.Amount > 0 ? $" plus ${trade.Amount}" : "")}", this.RemoveFromTrade));
        } else
            this.Options.Add(new("Trade", () => this.Menu.AddView(new TradeMenu(this._horse)), this._horse.IsInRace || this._horse.IsInMarket));

        return true;
    }

    private void RemoveFromMarket() => this._horse.BuyAnimal(this.Team);

    private void RemoveFromTrade() => this._horse.RemoveTrade();

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

    private class TradeMenu : View {

        private readonly Horse _horse;

        private Team? _traderTeam = null;
        private Horse? _traderHorse = null;
        private float _amount = 0f;

        private Team Team => this._horse.Team;

        public TradeMenu(Horse horse) {
            this._horse = horse;

            this.ReturnMessage = "Cancel";
        }

        public override bool RefreshView() {
            this.Header = new() {
                $"Day: {Game.Instance.Day}",
                $"Team: {this.Team.Name}",
                "",
                "- Trade details:",
                $"Team selected: {this._traderTeam?.Name}",
                $"Horse selected: {this._traderHorse?.Name}",
                $"Money amount: {this._amount}",
                ""
            };

            this.Options.Clear();

            this.Options.Add(new($"{(this._traderTeam == null ? "Select" : "Change")} team to trade", () => this.Menu.AddView(new TeamsView(this.ChangeTeamToTrade))));

            if (this._traderTeam != null) {
                this.Options.Add(new($"{(this._traderHorse == null ? "Select" : "Change")} horse to trade", () => this.Menu.AddView(new TeamHorsesView(this._traderTeam, (horse) => this.Menu.AddView(new TradeOfferPreviewView<Horse>(new Trade<Horse>(this._horse, 0, horse), () => this.ChangeTeamHorseToTrade(horse), null, "Change"))))));
                this.Options.Add(new($"Change amount of money", this.UpdatePrice));

                if (this._traderHorse != null)
                    this.Options.Add(new($"Send the trade offer", this.SendTradeOffer));
            }

            return true;
        }

        private void ChangeTeamToTrade(Team team) {
            this._traderTeam = team;
            this._traderHorse = null;
            this._amount = 0f;
            this.Menu.RemoveRecentView();
        }

        private void ChangeTeamHorseToTrade(Horse horse) {
            this._traderHorse = horse;
            this.Menu.RemoveRecentView(2);
        }

        private void UpdatePrice() {
            Console.Clear();
            Console.Write("Amount for the trade: ");
            float.TryParse(Console.ReadLine(), out this._amount);
            if (this._amount < 0) this._amount = 0;
        }

        private void SendTradeOffer() {
            this._horse.Trade(this._traderHorse, this._amount);
            this.Menu.RemoveRecentView();
        }

    }

}
