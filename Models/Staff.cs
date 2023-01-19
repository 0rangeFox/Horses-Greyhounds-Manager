namespace HaGManager.Models;
[Serializable]
public enum StaffType {

    Healer = 100,
    Trainer = 150

}
[Serializable]
public class StaffContract {

    public StaffType Type { get; }
    public int BoughtDate { get; }
    public int Duration { get; set; }

    public int RemainingDays => this.Duration - (Game.Instance.Day - this.BoughtDate);

    public StaffContract(StaffType type, int duration) {
        this.Type = type;
        this.BoughtDate = Game.Instance.Day;
        this.Duration = duration;
    }

}
