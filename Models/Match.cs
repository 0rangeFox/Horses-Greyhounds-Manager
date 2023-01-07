using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace HaGManager.Models;

[Serializable]
public class Match {

    public Guid ID = Guid.NewGuid();

    // Took the code from https://stackoverflow.com/a/42026123/9379900
    public string ShortID => Regex.Replace(Convert.ToBase64String(this.ID.ToByteArray()), "[/+=]", "");

    private List<Horse> _horses;
    public ReadOnlyCollection<Horse> Horses => this._horses.AsReadOnly();
    public int Capacity => this._horses.Capacity;
    public int Checkpoints { get; } // 1 Checkpoint = 100 Meters
    public int CreatedDate { get; }

    public Match(int date) {
        var random = new Random();
        this._horses = new List<Horse>(random.Next(4, 6));
        this.Checkpoints = random.Next(2, 15);
        this.CreatedDate = date;
    }

    // Game loop
    public void Start() {
        if (this._horses.Capacity == this._horses.Count) { }
    }

    public void AddHorse(Horse horse) {
        horse.Status = Status.Racing;
        this._horses.Add(horse);
    }

    public void RemoveHorse(Horse horse) {
        if (this._horses.Remove(horse))
            horse.Status = Status.None;
    }

}
