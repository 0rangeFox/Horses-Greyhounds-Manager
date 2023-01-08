using HaGManager.Models;

namespace HaGManager.Views; 

public class HorseMatchView : MatchView<Horse> {

    protected override Horse? JoinedAnimal => this.Match.Animals.FirstOrDefault(matchHorse => this.Team.Horses.Any(horse => horse.Status == Status.Racing && matchHorse.Equals(horse)));

    public HorseMatchView(Match<Horse> match) : base(match) {}

    protected override void RefreshMatchView() {
        this.Options.Clear();
        if (this.JoinedAnimal == null) {
            this.Options.Add(new (this.SelectedAnimal != null ? $"Selected horse: {this.SelectedAnimal.Name} - Change horse" : "Choose horse", () => this.Menu.AddView(new TeamHorsesView(UpdateHorse, true))));
            if (this.SelectedAnimal != null)
                this.Options.Add(new ("Join on this match", this.JoinMatch));
        } else this.Options.Add(new ("Leave this match", this.LeaveMatch));
    }

    private void UpdateHorse(Horse horse) {
        this.SelectedAnimal = horse;
        this.Menu.RemoveRecentView();
    }

}
