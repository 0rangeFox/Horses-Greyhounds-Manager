using HaGManager.Extensions;

namespace HaGManager.Models;

[Serializable]
public class Horse : Animal {

    public float Energy { get; }

    public Horse(string name) : base(name) {
        this.Energy = RandomExtension.NextSingle(0, 100);
    }

    public override bool BuyAnimal(Team buyer) {
        var seller = Game.Instance.Market.Find(seller => seller.Animal.Equals(this)) as Seller<Horse>;

        if (seller == null || !Game.Instance.Market.Remove(seller) || seller.Team == buyer) return false;

        seller.Team?.AddMoney(seller.Price);
        buyer.RemoveMoney(seller.Price);

        seller.Team?.Horses.Remove(seller.Animal);
        buyer.Horses.Add(seller.Animal);

        return true;
    }

    public override bool SellAnimal(float price, bool quickSell = false) {
        if (this.IsInRace || this.IsInTrade) return false;

        if (!quickSell) {
            Game.Instance.Market.Add(new Seller<Horse>(this, price));
            return true;
        }

        var team = this.Team;
        team.Horses.Remove(this);
        team.AddMoney(this.QuickSellPrice);

        return true;
    }

    public override bool Trade<A>(A? animalToTrade = null, float amount = 0f) where A: class {
        if (this.IsInRace || this.IsInMarket || this.IsInTrade || animalToTrade == null) return false;

        Game.Instance.Trades.Add(new Trade<Horse>(this, amount, (animalToTrade as Horse)!));
        return true;
    }

    public override bool RemoveTrade() => this.IsInTrade && Game.Instance.Trades.Remove(Game.Instance.Trades.First(trade => trade.FromAnimal.Equals(this)));

    public override bool AcceptTrade(Team team, float amount = 0f) {
        if (!this.Team.Horses.Contains(this)) return false;

        for (int i = Game.Instance.Trades.Count - 1; i >= 0; i--) {
            var trade = Game.Instance.Trades[i];

            if (trade.FromAnimal.Equals(this) || trade.ToAnimal.Equals(this))
                Game.Instance.Trades.RemoveAt(i);
        }

        var traderTeam = this.Team;
        traderTeam.RemoveMoney(amount);
        team.AddMoney(amount);

        traderTeam.Horses.Remove(this);
        team.Horses.Add(this);

        return true;
    }

}
