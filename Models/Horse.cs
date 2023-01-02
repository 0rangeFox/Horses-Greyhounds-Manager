using HaGManager.Extensions;

namespace HaGManager.Models;

public class Horse : Animal {

    public float Energy { get; }

    public Horse(string name) : base(name) {
        this.Energy = RandomExtension.NextSingle(0, 100);
    }

}
