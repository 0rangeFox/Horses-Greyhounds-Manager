using HaGManager.Extensions;

namespace HaGManager.Models;

public class Greyhound: Animal {

    public string Name { get; }
    public float Speed { get; }
    public float Energy { get; }
    public float Stamina { get; }
    public float Weight { get; }
    public List<Disease> Diseases { get; }

    public Greyhound(string name) {
        var random = new Random();
        this.Name = name;
        this.Speed = RandomExtension.NextSingle(0, 100, random);
        this.Energy = RandomExtension.NextSingle(0, 100, random);
        this.Stamina = RandomExtension.NextSingle(0, 100, random);
        this.Weight = RandomExtension.NextSingle(0, 100, random);
        this.Diseases = new List<Disease>();
    }

}