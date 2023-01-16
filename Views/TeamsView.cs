using HaGManager.Helpers.Views;
using HaGManager.Models;

namespace HaGManager.Views; 

public class TeamsView : View {

    private readonly Team _team = Game.Instance.ActualTeamPlaying;

    public TeamsView(Action<Team> selectTeamAction) {
        foreach (var team in Game.Instance.Teams) {
            this.Options.Add(new ($"Team: {team.Name}", () => selectTeamAction(team), team == this._team));
        }

        this.ReturnMessage = "Return to matches";
    }

}
