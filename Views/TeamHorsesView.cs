using HaGManager.Helpers.Views;
using HaGManager.Models;

namespace HaGManager.Views; 

public class TeamHorsesView : GView {

    private readonly bool _isManaging = false;

    public TeamHorsesView() {
        this._isManaging = true;
        
        this.ReturnMessage = "Return to menu";
    }

    public TeamHorsesView(Action<Horse> selectHorseAction) {
        foreach (var horse in this.Team.Horses) {
            var match = horse.GetMatch<Horse>();
            this.Options.Add(new ($"Name: {horse.Name} | {horse.Energy} {(match != null ? $"| Preparing on match {match.ShortID}" : "")}", () => selectHorseAction(horse), horse.IsInRace));
        }

        this.ReturnMessage = "Return to matches";
    }

    public TeamHorsesView(Team team, Action<Horse> selectHorseAction) {
        this.Team = team;

        foreach (var horse in this.Team.Horses)
            this.Options.Add(new ($"Name: {horse.Name} | {horse.Energy}", () => selectHorseAction(horse), horse.IsInRace));

        this.ReturnMessage = "Back";
    }

    public override bool RefreshView() {
        if (!this._isManaging) return true;

        this.Header = new() {
            $"Day: {Game.Instance.Day}",
            $"Team: {this.Team.Name}",
            $"Horses owned: {this.Team.Horses.Count}",
            ""
        };

        this.Options.Clear();
        foreach (var horse in this.Team.Horses) {
            var seller = horse.GetSeller<Horse>();
            this.Options.Add(new ($"Name: {horse.Name} | {horse.Energy} {(seller != null ? $"| Selling on market for ${seller.Price}" : "")}", () => this.Menu.AddView(new TeamHorseInspectView(horse)), horse.IsInRace));
        }

        return true;
    }

}
