using HaGManager.Extensions;

namespace HaGManager.Models;

[Serializable]
public class Horse : Animal {

    public float Energy { get; }

    public Horse(string name) : base(name) {
        this.Energy = RandomExtension.NextSingle(0, 100);
    }

    public override void BuyAnimal(Team buyer) {
        var seller = Game.Instance.Market.Find(seller => seller.Animal.Equals(this)) as Seller<Horse>;

        if (seller == null || !Game.Instance.Market.Remove(seller) || seller.Team == buyer) return;

        seller.Team?.AddMoney(seller.Price);
        buyer.RemoveMoney(seller.Price);

        seller.Team?.Horses.Remove(seller.Animal);
        buyer.Horses.Add(seller.Animal);
    }

    public override void SellAnimal(float price, bool quickSell = false) {
        if (!quickSell) {
            Game.Instance.Market.Add(new Seller<Horse>(this, price));
            return;
        }

        var team = this.Team;
        team.Horses.Remove(this);
        team.AddMoney(this.QuickSellPrice);
    }

}
