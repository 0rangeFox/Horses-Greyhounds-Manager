using HaGManager.Helpers.Views;
using HaGManager.Models;

namespace HaGManager.Views; 

public abstract class MatchView<A> : View where A: Animal {

    protected readonly Team Team = Game.Instance.ActualTeamPlaying;
    protected readonly Match<A> Match;

    protected A? SelectedAnimal = null;
    protected virtual A? JoinedAnimal => null;

    protected MatchView(Match<A> match) {
        this.Match = match;

        this.ReturnMessage = "Return to matches";
    }

    protected virtual void RefreshMatchView() {}

    public override void RefreshView() {
        this.Header = new() {
            $"Day: {Game.Instance.Day}",
            $"Team: {this.Team.Name}",
            "",
            $"Match ID: {this.Match.ID} ({this.Match.ShortID})",
            $"Track range: {this.Match.Checkpoints * 100} meters",
            $"Players: {this.Match.Animals.Count}/{this.Match.Capacity}",
            $"Created Date: {this.Match.CreatedDate}",
            ""
        };

        this.RefreshMatchView();
    }

    protected void JoinMatch() {
        if (this.SelectedAnimal == null) return;
        this.Match.AddAnimal(this.SelectedAnimal);
        this.SelectedAnimal = null;
    }

    protected void LeaveMatch() {
        this.Match.RemoveAnimal(this.JoinedAnimal);
    }

}
