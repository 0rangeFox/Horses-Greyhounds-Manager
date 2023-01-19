using HaGManager.Helpers.Views;

namespace HaGManager.Views; 

public class StaffView : GView {

    public StaffView() {
        this.ReturnMessage = "Return to menu";
    }

    public override bool RefreshView() {
        this.Header = new() {
            Game.Instance.DayDescription,
            $"Team: {this.Team.Name}"
        };

        foreach (var staff in this.Team.Staffs.Values)
            this.Options.Add(new ViewOption($"Staff type: {staff.Type} | Days left: {staff.RemainingDays}", null, true));

        return true; 
    }

}
