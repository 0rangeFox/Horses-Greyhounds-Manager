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

    public string Name { get; }
    public float Speed { get; }
    public float Resistance { get; }
    public float Weight { get; }

    public List<Disease> Diseases { get; }

    public Status Status { get; set; } = Status.None;

    protected Animal(string name) {
        var random = new Random();

        this.Name = name;
        this.Speed = RandomExtension.NextSingle(0, 100, random);
        this.Resistance = RandomExtension.NextSingle(0, 100, random);
        this.Weight = RandomExtension.NextSingle(0, 100, random);

        this.Diseases = new List<Disease>();
    }    

}
