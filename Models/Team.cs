namespace HaGManager.Models;

public class Team {

    public static Dictionary<int, Reward> Rewards = new Dictionary<int, Reward> {
        [1000] = Reward.HorseSlot,
        //[2000] = Reward.HorseSlot,
        [3000] = Reward.Money500,
        [4000] = Reward.HorseSlot,
        [5000] = Reward.HorseSlot
    };

    public string Name { get; }
    public float Balance { get; private set; } = 500f;

    public Dictionary<StaffType, StaffContract> Staffs = new Dictionary<StaffType, StaffContract>();
    public List<Horse> Horses { get; } = new List<Horse>();
    public Greyhound Greyhound { get; }

    // Stats
    public int Experience { get; private set; } = 0;
    public int Wins { get; private set; }
    public int Loses { get; private set; }

    public Team(string name, string horseName, string greyhoundName) {
        this.Name = name;
        this.Horses.Add(new Horse(horseName));
        this.Greyhound = new Greyhound(greyhoundName);
    }

    // 1000 = 1 Horse Slot
    // 2000 
    // 3000
    // 4000 = 1 Horse Slot <-
    // 5000 = 1 Horse Slot

    public int GetMaxHorsesSlots() {
        var count = 1;

        foreach (var reward in Rewards)
            if (reward.Value == Reward.HorseSlot && this.Experience >= reward.Key)
                count++;

        return count;
    }

    public int GetFreeHorsesSlots() {
        return this.GetMaxHorsesSlots() - this.Horses.Count;
    }

    public float AddMoney(float amount) {
        return this.Balance += amount;
    }

    
    // TODO Fix this code to not repeat the old rewards.
    public int AddExperience(int amount) {
        //this.Experience += amount;

        foreach (var reward in Rewards)
            // if (reward.Value is Reward.Money500 or Reward.Money1000 && this.Experience >= reward.Key) {
            //     this.Balance += (int) reward.Value;
            //     Console.WriteLine($"The team \"{this.Name}\" has leveled up!");
            // }
            if (this.Experience < reward.Key) {
                this.Experience += amount;
            } //else if ()
        
        

        return this.Experience;
    }

}
