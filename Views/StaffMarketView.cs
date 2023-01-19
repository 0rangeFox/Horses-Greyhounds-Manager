using HaGManager.Helpers.Views;
using HaGManager.Models;

namespace HaGManager.Views; 

public class StaffMarketView : View {

    private readonly Team _team = Game.Instance.ActualTeamPlaying;

    public StaffMarketView() {
        this.ReturnMessage = "Return to menu";
    }

    public override bool RefreshView() {
        this.Header = new() {
            $"Day: {Game.Instance.Day}",
            $"Team: {this._team.Name}",
            $"Balance: {this._team.Balance}",
            ""
        };

        this.Options.Clear();
        foreach (var staff in Enum.GetValues<StaffType>())
            this.Options.Add(new ViewOption($"Staff type: {staff} | Price: {(int)staff}/day", () => this.Menu.AddView(new DaysSelector(staff))));

        return true;
    }

    private class DaysSelector : View {

        private static readonly int[] Times = { 1, 3, 7, 15, 30 };

        private readonly Team _team = Game.Instance.ActualTeamPlaying;

        public DaysSelector(StaffType staff) {
            foreach (var day in Times)
                this.Options.Add(new ViewOption($"{day} Day: {(int) staff * day}", () => this.BuyContract(staff, day), this._team.Balance < (int) staff * day));
        }

        private void BuyContract(StaffType staff, int day) {
            this._team.RemoveMoney((int) staff * day);
            this._team.AddStaffContract(new StaffContract(staff, day));
            this.Menu.RemoveRecentView();
        }

    }

}
