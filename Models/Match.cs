using System.Text.RegularExpressions;

namespace HaGManager.Models;

public class Match {

    public Guid ID = Guid.NewGuid();

    // Took the code from https://stackoverflow.com/a/42026123/9379900
    public string ShortID => Regex.Replace(Convert.ToBase64String(this.ID.ToByteArray()), "[/+=]", "");  

    public List<Team> Teams { get; }
    public int Checkpoints { get; } // 1 Checkpoint = 100 Meters
    public int CreatedDate { get; }

    public Match(int date) {
        var random = new Random();
        this.Teams = new List<Team>(random.Next(4, 6));
        this.Checkpoints = random.Next(2, 15);
        this.CreatedDate = date;
    }

    // Game loop
    public void Start() {
        if (this.Teams.Capacity == this.Teams.Count) { }
    }

}
