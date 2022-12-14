namespace HaGManager.Models; 

public class Match {

    public List<Team> Teams { get; }
    public int Checkpoints { get; } // 1 Checkpoint = 100 Meters
    public int CreatedDate { get; }

    public Match(int date) {
        var random = new Random();
        Teams = new List<Team>(random.Next(4, 6));
        Checkpoints = random.Next(2, 15);
        CreatedDate = date;
    }

    // Game loop
    public void Start() {
        if (this.Teams.Capacity == this.Teams.Count) {
            
        }
    }

}
