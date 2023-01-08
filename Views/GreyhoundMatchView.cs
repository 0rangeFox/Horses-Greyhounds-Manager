using HaGManager.Models;

namespace HaGManager.Views; 

public class GreyhoundMatchView : MatchView<Greyhound> {

    protected override Greyhound? JoinedAnimal => this.Match.Animals.FirstOrDefault(matchGreyhound => this.Team.Greyhound.Status == Status.Racing && matchGreyhound.Equals(this.Team.Greyhound));

    public GreyhoundMatchView(Match<Greyhound> match) : base(match) {}

    protected override void RefreshMatchView() {
        this.Options.Clear();
        if (this.JoinedAnimal == null) {
            this.Options.Add(new (this.SelectedAnimal != null ? $"Selected greyhound: {this.SelectedAnimal.Name} - Change greyhound" : "Choose greyhound", () => this.Menu.AddView(new TeamGreyhoundsView(UpdateGreyhound, true))));
            if (this.SelectedAnimal != null)
                this.Options.Add(new ("Join on this match", this.JoinMatch));
        } else this.Options.Add(new ("Leave this match", this.LeaveMatch));
    }

    private void UpdateGreyhound(Greyhound greyhound) {
        this.SelectedAnimal = greyhound;
        this.Menu.RemoveRecentView();
    }

}
