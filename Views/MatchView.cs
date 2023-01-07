using HaGManager.Helpers.Views;
using HaGManager.Models;

namespace HaGManager.Views; 

public class MatchView : View {

    private readonly Team _team = Game.Instance.ActualTeamPlaying;
    private readonly Match _match;
    private Horse? _selectedHorse = null;

    private Horse? GetHorseJoined() => this._match.Horses.FirstOrDefault(matchHorse => this._team.Horses.Any(horse => horse.Status == Status.Racing && matchHorse == horse));

    public MatchView(Match match) {
        this._match = match;

        this.ReturnMessage = "Return to matches";
    }

    private void UpdateHorse(Horse horse) {
        this._selectedHorse = horse;
        this.Menu.RemoveRecentView();
    }

    public override void RefreshView() {
        this.Header = new() {
            $"Day: {Game.Instance.Time}",
            $"Team: {this._team.Name}",
            "",
            $"Match ID: {this._match.ID} ({this._match.ShortID})",
            $"Track range: {this._match.Checkpoints * 100} meters",
            $"Players: {this._match.Horses.Count}/{this._match.Capacity}",
            $"Created Date: {this._match.CreatedDate}",
            ""
        };

        this.Options.Clear();
        if (this.GetHorseJoined() == null) {
            this.Options.Add(new (this._selectedHorse != null ? $"Selected horse: {this._selectedHorse.Name} - Change horse" : "Choose horse", () => this.Menu.AddView(new TeamHorsesView(UpdateHorse, true))));
            if (this._selectedHorse != null)
                this.Options.Add(new ("Join on this match", JoinMatch));
        } else this.Options.Add(new ("Leave this match", LeaveMatch));
    }

    private void JoinMatch() {
        this._match.AddHorse(this._selectedHorse!);
        this._selectedHorse = null;
    }

    private void LeaveMatch() {
        this._match.RemoveHorse(this.GetHorseJoined()!);
    }

}
