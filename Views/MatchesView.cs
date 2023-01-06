using HaGManager.Helpers.Views;
using HaGManager.Models;

namespace HaGManager.Views; 

public class MatchesView : View {

    private readonly Team _team = Game.Instance.ActualTeamPlaying;

    public MatchesView() {
        this.Header = new() {
            $"Day: {Game.Instance.Time}",
            $"Team: {this._team.Name}",
            $"Matches: {Game.Instance.Matches.Count}",
            ""
        };

        for (int i = 0; i < Game.Instance.Matches.Count; i++) {
            var match = Game.Instance.Matches[i];
            this.Options.Add(new ViewOption($"Match ID: {match.ShortID} | Track range: {match.Checkpoints * 100} meters | Players: {match.Teams.Count}/{match.Teams.Capacity}", () => this.Menu.AddView(new MatchView(match))));
        }

        this.ReturnMessage = "Return to menu";
    }

}