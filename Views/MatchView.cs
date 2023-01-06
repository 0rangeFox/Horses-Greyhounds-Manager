using HaGManager.Helpers.Views;
using HaGManager.Models;

namespace HaGManager.Views; 

public class MatchView : View {

    private readonly Team _team = Game.Instance.ActualTeamPlaying;
    private readonly Match _match;
    private Horse? _selectedHorse = null;

    public MatchView(Match match) {
        this._match = match;

        this.Header = new() {
            $"Day: {Game.Instance.Time}",
            $"Team: {this._team.Name}",
            "",
            $"Match ID: {this._match.ID} ({this._match.ShortID})",
            $"Track range: {this._match.Checkpoints * 100} meters",
            $"Players: {this._match.Teams.Count}/{this._match.Teams.Capacity}",
            $"Created Date: {this._match.CreatedDate}",
            ""
        };

        this.ReturnMessage = "Return to matches";
    }

    private void UpdateHorse(Horse horse) {
        this._selectedHorse = horse;
        this.Menu.RemoveRecentView();
        this.RefreshView();
    }

    public override void RefreshView() {
        this.Options.Clear();
        var viewOptions = new List<ViewOption>() {
            new (this._selectedHorse != null ? $"Selected horse: {this._selectedHorse.Name} - Change horse" : "Choose horse", () => this.Menu.AddView(new TeamHorsesView(UpdateHorse)))
        };

        if (this._selectedHorse != null)
            viewOptions.Add(new ("Join on this match"));

        this.Options.AddRange(viewOptions);
    }

}
