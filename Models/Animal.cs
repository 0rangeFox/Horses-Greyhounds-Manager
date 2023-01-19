using HaGManager.Extensions;

namespace HaGManager.Models;

[Serializable]
public enum Disease {

    BrokenBone,
    Sick,
    Fatigue

}

[Serializable]
public enum Status {

    None,
    Racing

}

[Serializable]
public abstract class Animal {

    private const float QuickSellFee = .25f; // In Percentage

    public Guid ID { get; } = Guid.NewGuid();

    public string Name { get; }
    public float Speed { get; }
    public float Resistance { get; }
    public float Weight { get; }

    public List<Disease> Diseases { get; }

    public Status Status { get; set; } = Status.None;

    public Team Team => Game.Instance.Teams.First(team => this is Horse && team.Horses.Contains(this) || this is Greyhound && team.Greyhound.Equals(this));
    public bool IsInRace => this.Status == Status.Racing;
    public bool IsInMarket => Game.Instance.Market.Exists(seller => seller.Animal.Equals(this));
    public bool IsInTrade => Game.Instance.Trades.Exists(trade => trade.FromAnimal.Equals(this));
    public float Price => ((this.Speed * this.Resistance) / this.Weight) * 100;
    public float QuickSellPrice => this.Price * (1f - QuickSellFee);

    protected Animal(string name) {
        this.Name = name;
        this.Speed = RandomExtension.NextSingle(5, 30);
        this.Resistance = RandomExtension.NextSingle(5, 30);
        this.Weight = RandomExtension.NextSingle(10, 30);
        this.Diseases = new List<Disease>();
    }

    public abstract bool BuyAnimal(Team buyer);
    public abstract bool SellAnimal(float price, bool quickSell = false);
    public abstract bool Trade(Animal? animalToTrade = null, float amount = 0f);
    public abstract bool RemoveTrade();
    public abstract bool AcceptTrade(Team team, float amount = 0f);

    public virtual bool CancelTrade(ITrade<Animal> trade) => Game.Instance.Trades.Remove(trade);

    public Match<A>? GetMatch<A>() where A: Animal {
        if (this.IsInRace)
            foreach (var gameMatch in Game.Instance.Matches)
                if (gameMatch is Match<A> match && match.Animals.Contains(this))
                    return match;
        return null;
    }

    public Seller<A>? GetSeller<A>() where A : Animal {
        if (this.IsInMarket)
            foreach (var marketSeller in Game.Instance.Market)
                if (marketSeller is Seller<A> seller && seller.Animal.Equals(this))
                    return seller;
        return null;
    }

    public Trade<A>? GetTrade<A>() where A : Animal {
        if (this.IsInTrade)
            foreach (var iTrade in Game.Instance.Trades)
                if (iTrade is Trade<A> trade && trade.FromAnimal.Equals(this))
                    return trade;
        return null;
    }

    public override int GetHashCode() => this.ID.GetHashCode();
    public override bool Equals(object? obj) => obj is Animal animal && this.ID.Equals(animal.ID);

}
