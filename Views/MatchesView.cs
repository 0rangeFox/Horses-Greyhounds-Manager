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

        this.ReturnMessage = "Return to menu";
    }

    public override void RefreshView() {
        this.Options.Clear();
        foreach (var gameMatch in Game.Instance.Matches) {
            switch (gameMatch) {
                case Match<Horse> match:
                    this.Options.Add(new ViewOption($"Match ID: {match.ShortID} | Track range: {match.Checkpoints * 100} meters | Players: {match.Animals.Count}/{match.Capacity} | Type: Horse", () => this.Menu.AddView(new HorseMatchView(match))));
                    break;
                case Match<Greyhound> match:
                    this.Options.Add(new ViewOption($"Match ID: {match.ShortID} | Track range: {match.Checkpoints * 100} meters | Players: {match.Animals.Count}/{match.Capacity} | Type: Greyhound", () => this.Menu.AddView(new GreyhoundMatchView(match))));
                    break;
            }
        }
    }

}
