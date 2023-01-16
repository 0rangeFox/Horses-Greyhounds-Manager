namespace HaGManager.Models;

public interface ITrade<out A> where A : Animal {

    public A FromAnimal { get; }
    public A ToAnimal { get; }
    public float Amount { get; }
    public Team FromTeam { get; }
    public Team ToTeam { get; }

}

[Serializable]
public class Trade<A> : ITrade<A> where A: Animal {

    public A FromAnimal { get; }
    public A ToAnimal { get; }
    public float Amount { get; }
    public Team FromTeam => this.FromAnimal.Team;
    public Team ToTeam => this.ToAnimal.Team;

    public Trade(A fromAnimal, float amount, A toAnimal) {
        this.FromAnimal = fromAnimal;
        this.Amount = amount;
        this.ToAnimal = toAnimal;
    }

}
