namespace HaGManager.Models;

public enum Disease {
    BoneBroken
}

public interface Animal {

    string Name { get; }
    float Speed { get; }
    float Stamina { get; }
    float Energy { get; }
    float Weight { get; }
    List<Disease> Diseases { get; }

}
