using HaGManager.Helpers.Views;
using HaGManager.Models;

namespace HaGManager.Views; 

public class TeamsView : GView {

    public TeamsView(Action<Team> selectTeamAction) {
        foreach (var team in Game.Instance.Teams)
            this.Options.Add(new ($"Team: {team.Name}", () => selectTeamAction(team), team == this.Team));

        this.ReturnMessage = "Return to matches";
    }

}
