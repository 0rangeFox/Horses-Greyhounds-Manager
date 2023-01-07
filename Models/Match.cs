using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace HaGManager.Models;

public interface IMatch<out A> where A: Animal { }

[Serializable]
public class Match<A> : IMatch<A> where A: Animal {

    public Guid ID = Guid.NewGuid();

    // Took the code from https://stackoverflow.com/a/42026123/9379900
    public string ShortID => Regex.Replace(Convert.ToBase64String(this.ID.ToByteArray()), "[/+=]", "");

    private List<A> _animals;
    public ReadOnlyCollection<A> Animals => this._animals.AsReadOnly();
    public int Capacity => this._animals.Capacity;
    public int Checkpoints { get; } // 1 Checkpoint = 100 Meters
    public int CreatedDate { get; }

    public Match(int date) {
        var random = new Random();
        this._animals = new List<A>(random.Next(4, 6));
        this.Checkpoints = random.Next(2, 15);
        this.CreatedDate = date;
    }

    // Game loop
    public void Start() {
        if (this._animals.Capacity == this._animals.Count) { }
    }

    public void AddAnimal(A animal) {
        animal.Status = Status.Racing;
        this._animals.Add(animal);
    }

    public void RemoveAnimal(A animal) {
        if (this._animals.Remove(animal))
            animal.Status = Status.None;
    }

}
