namespace HaGManager.Models;

public interface ISeller<out A> where A : Animal {

    public Team? Team { get; }
    public A Animal { get; }
    public float Price { get; }

}

[Serializable]
public class Seller<A> : ISeller<A> where A: Animal {

    public Team? Team { get; } = null;
    public A Animal { get; }
    public float Price { get; }

    public Seller(A animal) {
        this.Animal = animal;
        this.Price = this.Animal.Price;
    }

    public Seller(A animal, float price) {
        this.Team = animal.Team;
        this.Animal = animal;
        this.Price = price;
    }

}
