namespace HaGManager.Models; 

public enum StaffType {

    Healer,
    Trainer

}

public class StaffContract {

    public StaffType Type { get; }
    public int BoughtDate { get; set; }
    public int Duration { get; }

}
