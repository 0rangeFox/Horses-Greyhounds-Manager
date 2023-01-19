using HaGManager.Helpers.Views;
using HaGManager.Models;

namespace HaGManager.Views; 

public class StaffView : View {

    private readonly Team _team = Game.Instance.ActualTeamPlaying;

    public StaffView() {
        this.ReturnMessage = "Return to menu";
    }

    public override bool RefreshView() {
        this.Header = new() {
            $"Day: {Game.Instance.Day}",
            $"Team: {this._team.Name}"
        };

        foreach (var staff in _team.Staffs.Values)
            this.Options.Add(new ViewOption($"Staff type: {staff.Type} | Days left: {staff.RemainingDays}", null, true));

        return true; 
    }

}
