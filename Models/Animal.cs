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

    private static float _quickSellFee = .25f; // In Percentage

    public Guid ID { get; } = Guid.NewGuid();

    public string Name { get; }
    public float Speed { get; }
    public float Resistance { get; }
    public float Weight { get; }

    public List<Disease> Diseases { get; }

    public Status Status { get; set; } = Status.None;

    public Team Team => Game.Instance.Teams.First(team => this is Horse && team.Horses.Contains(this) || this is Greyhound && team.Greyhound.Equals(this));
    public bool IsInMarket => Game.Instance.Market.Exists(seller => seller.Animal.Equals(this));
    public float Price => ((this.Speed * this.Resistance) / this.Weight) * 100;
    public float QuickSellPrice => this.Price * (1f - _quickSellFee);

    public Match<A>? GetMatch<A>() where A: Animal {
        if (this.Status == Status.Racing)
            foreach (var gameMatch in Game.Instance.Matches)
                if (gameMatch is Match<A> match && match.Animals.Contains(this))
                    return match;
        return null;
    }

    protected Animal(string name) {
        this.Name = name;
        this.Speed = RandomExtension.NextSingle(5, 30);
        this.Resistance = RandomExtension.NextSingle(5, 30);
        this.Weight = RandomExtension.NextSingle(10, 30);
        this.Diseases = new List<Disease>();
    }

    public abstract void BuyAnimal(Team buyer);
    public abstract void SellAnimal(float price, bool quickSell = false);

    public override int GetHashCode() => this.ID.GetHashCode();
    public override bool Equals(object? obj) => obj is Animal animal && this.ID.Equals(animal.ID);

}
