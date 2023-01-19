using HaGManager.Helpers.Views;

namespace HaGManager.Views; 

public class StaffView : View {

    public StaffView() {
        this.ReturnMessage = "Return to menu";
    }

    public override bool RefreshView() {
        this.Header = new() {
            $"Day: {Game.Instance.Day}",
            $"Team: {this.Team.Name}"
        };

        foreach (var staff in this.Team.Staffs.Values)
            this.Options.Add(new ViewOption($"Staff type: {staff.Type} | Days left: {staff.RemainingDays}", null, true));

        return true; 
    }

}
