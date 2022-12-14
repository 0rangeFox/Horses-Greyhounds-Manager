namespace HaGManager.Models; 

public class Team {

    public string Name { get; }
    public float Balance { get; private set; } = 500f;

    public Dictionary<StaffType, StaffContract> Staffs = new Dictionary<StaffType, StaffContract>();
    public List<Horse> Horses { get; } = new List<Horse>();
    public Greyhound Greyhound { get; }

    // Stats
    public int Experience { get; private set; }
    public int Wins { get; private set; }
    public int Loses { get; private set; }

    // 1000 = 1 Horse Slot
    // 2000 
    // 3000
    // 4000 = 1 Horse Slot <-
    // 5000 = 1 Horse Slot

    public int GetMaxHorsesSlots() {
        // Lista, 

        return 1;
    }

    public int GetFreeHorsesSlots() {
        return this.GetMaxHorsesSlots() - this.Horses.Count;
    }

    public float AddMoney(float amount) {
        return this.Balance += amount;
    }

}
