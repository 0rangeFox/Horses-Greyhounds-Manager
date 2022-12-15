namespace HaGManager.Models;

using HaGManager.Extensions;

public class Horse: Animal {

    float Energy { get; }

    public Horse(string name) : base(name) {
        this.Energy = RandomExtension.NextSingle(0, 100);
    }

}
