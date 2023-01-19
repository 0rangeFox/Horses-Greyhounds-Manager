namespace HaGManager.Models;

[Serializable]
public class Team {

    public static Dictionary<int, Reward> Rewards = new() {
        [1000] = Reward.HorseSlot,
        //[2000] = ,
        [3000] = Reward.Money500,
        [4000] = Reward.HorseSlot,
        //[5000] = ,
        //[6000] = ,
        [7000] = Reward.Money1000,
        //[8000] = 
    };

    public string Name { get; }
    public float Balance { get; private set; } = 500f;
    public List<Horse> Horses { get; } = new();
    public Greyhound Greyhound { get; }
    public Dictionary<StaffType, StaffContract> Staffs { get; } = new();

    public bool TrainedAlready { get; set; } = false;

    // Stats
    public int Experience { get; private set; }
    public int Wins { get; private set; }
    public int Loses { get; private set; }

    public Team(string name, string horseName, string greyhoundName) {
        this.Name = name;
        this.Horses.Add(new Horse(horseName));
        this.Greyhound = new Greyhound(greyhoundName);
    }

    public int GetMaxHorsesSlots() {
        var count = 1;

        foreach (var reward in Rewards)
            if (reward.Value == Reward.HorseSlot && this.Experience >= reward.Key)
                count++;

        return count;
    }

    public int GetFreeHorsesSlots() {
        return GetMaxHorsesSlots() - this.Horses.Count;
    }

    public float AddMoney(float amount) {
        return this.Balance += amount;
    }

    public float RemoveMoney(float amount) {
        if ((this.Balance -= amount) < 0)
            this.Balance = 0;
        return this.Balance;
    }

    public void AddStaffContract(StaffContract contract) {
        if (Staffs.ContainsKey(contract.Type))
            this.Staffs[contract.Type].Duration += contract.Duration;
        else this.Staffs.Add(contract.Type, contract);
    }

    public bool RemoveStaff(StaffType type) => this.Staffs.Remove(type);

    public int AddExperience(int amount) {
        foreach (var reward in Rewards)
            if (reward.Value is Reward.Money500 or Reward.Money1000 && this.Experience < reward.Key && this.Experience + amount >= reward.Key)
                this.Balance += (int) reward.Value;

        return this.Experience += amount;
    }

    public int AddWin() => this.Wins++;
    public int AddLose() => this.Loses++;

}
