using HaGManager.Extensions;

namespace HaGManager.Models;

[Serializable]
public enum Disease {

    BoneBroken

}

[Serializable]
public enum Status {

    None,
    Racing

}

[Serializable]
public abstract class Animal {

    public Guid ID { get; } = Guid.NewGuid();

    public string Name { get; }
    public float Speed { get; }
    public float Resistance { get; }
    public float Weight { get; }

    public List<Disease> Diseases { get; }

    public Status Status { get; set; } = Status.None;

    public Team Team => Game.Instance.Teams.First(team => this is Horse && team.Horses.Contains(this) || this is Greyhound && team.Greyhound.Equals(this));

    public Match<A>? GetMatch<A>() where A: Animal {
        if (this.Status == Status.Racing)
            foreach (var gameMatch in Game.Instance.Matches)
                if (gameMatch is Match<A> match && match.Animals.Contains(this))
                    return match;
        return null;
    }

    protected Animal(string name) {
        var random = new Random();

        this.Name = name;
        this.Speed = RandomExtension.NextSingle(0, 100, random);
        this.Resistance = RandomExtension.NextSingle(0, 100, random);
        this.Weight = RandomExtension.NextSingle(0, 100, random);

        this.Diseases = new List<Disease>();
    }

    public override int GetHashCode() => this.ID.GetHashCode();
    public override bool Equals(object? obj) => obj is not Animal animal || this.ID.Equals(animal.ID);

}
