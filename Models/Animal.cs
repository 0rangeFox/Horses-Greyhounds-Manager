using HaGManager.Extensions;

namespace HaGManager.Models;

public enum Disease {

    BoneBroken

}

public abstract class Animal {

    public string Name { get; }
    public float Speed { get; }
    public float Resistance { get; }
    public float Weight { get; }

    public List<Disease> Diseases { get; }

    protected Animal(string name) {
        var random = new Random();

        this.Name = name;
        this.Speed = RandomExtension.NextSingle(0, 100, random);
        this.Resistance = RandomExtension.NextSingle(0, 100, random);
        this.Weight = RandomExtension.NextSingle(0, 100, random);

        this.Diseases = new List<Disease>();
    }    

}
