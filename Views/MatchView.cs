using HaGManager.Helpers.Views;
using HaGManager.Models;

namespace HaGManager.Views; 

public abstract class MatchView<A> : GView where A: Animal {

    protected readonly Match<A> Match;

    protected A? SelectedAnimal = null;
    protected virtual A? JoinedAnimal => null;

    protected MatchView(Match<A> match) {
        this.Match = match;

        this.ReturnMessage = "Return to matches";
    }

    protected virtual void RefreshMatchView() {}

    public override bool RefreshView() {
        this.Header = new() {
            Game.Instance.DayDescription,
            $"Team: {this.Team.Name}",
            "",
            $"Match ID: {this.Match.ID} ({this.Match.ShortID})",
            $"Track range: {this.Match.Checkpoints * 100} meters",
            $"Players: {this.Match.Animals.Count}/{this.Match.Capacity}",
            $"Created Date: {this.Match.CreatedDate}",
            ""
        };

        this.Header.Add("Rewards:");
        for (int i = 0; i < this.Match.Capacity; i++)
            this.Header.Add($"#{i + 1} | Money: {this.Match.MoneyRewards[i]} | Experience: {this.Match.ExperienceRewards[i]}");
        this.Header.Add("");

        this.RefreshMatchView();

        return true;
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
