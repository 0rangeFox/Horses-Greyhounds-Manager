namespace HaGManager.Models;

public interface ISeller<out A> where A : Animal {

    public A Animal { get; }
    public float Price { get; }

    public Team? Team { get; }

}

[Serializable]
public class Seller<A> : ISeller<A> where A: Animal {

    public A Animal { get; }
    public float Price { get; }

    public Team? Team {
        get {
            try {
                return this.Animal.Team;
            } catch (Exception e) {
                return null;
            }
        }
    }

    public Seller(A animal, float? price = null) {
        this.Animal = animal;
        this.Price = price ?? this.Animal.Price;
    }

}
