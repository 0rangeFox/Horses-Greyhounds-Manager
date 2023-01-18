namespace HaGManager.Models;

public enum StaffType {

    Healer = 100,
    Trainer = 150

}

public class StaffContract {

    public StaffType Type { get; }
    public int BoughtDate { get; }
    public int Duration { get; set; }
    public int RemainingDays => this.Duration - (Game.Instance.Day - this.BoughtDate);

    public StaffContract(StaffType type, int duration)
    {
        this.Type = type;
        this.Duration = duration;
        this.BoughtDate = Game.Instance.Day;
    }


}
