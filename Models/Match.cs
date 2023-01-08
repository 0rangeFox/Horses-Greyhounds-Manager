using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace HaGManager.Models;

public interface IMatch<out A> where A : Animal {

    public Guid ID { get; }
    public string ShortID { get; }

    public int Capacity { get; }
    public int Checkpoints { get; } // 1 Checkpoint = 100 Meters
    public int CreatedDate { get; }
    public int Duration { get; }
    public int RemainingDays { get; }

    public void Start();
    public void End();

}

[Serializable]
public class Match<A> : IMatch<A> where A: Animal {

    private static int _minPlayers = 4;
    private static int _maxPlayers = 6;
    private static int _minCheckpoints = 3;
    private static int _maxCheckpoints = 15;
    private static int _maxDuration = 14; // Days

    public Guid ID { get; } = Guid.NewGuid();
    // Took the code from https://stackoverflow.com/a/42026123/9379900
    public string ShortID => Regex.Replace(Convert.ToBase64String(this.ID.ToByteArray()), "[/+=]", "");

    private List<A> _animals;
    public ReadOnlyCollection<A> Animals => this._animals.AsReadOnly();
    public int Capacity => this._animals.Capacity;
    public int Checkpoints { get; }
    public int CreatedDate { get; }
    public int Duration { get; }
    public int RemainingDays => (this.CreatedDate + this.Duration) - Game.Instance.Time;

    public Match() {
        var random = new Random();
        this._animals = new List<A>(random.Next(_minPlayers, _maxPlayers));
        this.Checkpoints = random.Next(_minCheckpoints, _maxCheckpoints);
        this.CreatedDate = Game.Instance.Time;
        this.Duration = random.Next(1, _maxDuration);
    }

    public void AddAnimal(A animal) {
        animal.Status = Status.Racing;
        this._animals.Add(animal);
    }

    public void RemoveAnimal(A animal) {
        if (this._animals.Remove(animal))
            animal.Status = Status.None;
    }

    public void Start() {
        if (this.RemainingDays - 1 > 0 || this._animals.Capacity < this._animals.Count) return;

        // Game logic

        this.End();
    }

    public void End() {
        foreach (var animal in this._animals)
            this.RemoveAnimal(animal);

        Game.Instance.Matches.Remove(this);
    }

    public override string ToString() => $"Match ID: {this.ShortID} | Track range: {this.Checkpoints * 100} meters | Players: {this.Animals.Count}/{this.Capacity} | {(this.RemainingDays > 1 ? $"Subscriptions will close in {this.RemainingDays} days" : "Last day to subscribe")}.";
    public override int GetHashCode() => this.ID.GetHashCode();
    public override bool Equals(object? obj) => obj is not Match<A> match || this.ID.Equals(match.ID);

}
