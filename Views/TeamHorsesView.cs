using HaGManager.Helpers.Views;
using HaGManager.Models;

namespace HaGManager.Views; 

public class TeamHorsesView : View {

    private readonly Team _team = Game.Instance.ActualTeamPlaying;
    private bool _isManaging = false;

    public TeamHorsesView() {
        this._isManaging = true;
        
        this.ReturnMessage = "Return to menu";
    }

    public TeamHorsesView(Action<Horse> selectHorseAction) {
        foreach (var horse in this._team.Horses) {
            var match = horse.GetMatch<Horse>();
            this.Options.Add(new ($"Name: {horse.Name} | {horse.Energy} {(match != null ? $"| Preparing on match {match.ShortID}" : "")}", () => selectHorseAction(horse), horse.Status == Status.Racing));
        }

        this.ReturnMessage = "Return to matches";
    }

    public override bool RefreshView() {
        if (!this._isManaging) return true;

        this.Header = new() {
            $"Day: {Game.Instance.Day}",
            $"Team: {this._team.Name}",
            $"Horses owned: {this._team.Horses.Count}",
            ""
        };

        this.Options.Clear();
        foreach (var horse in this._team.Horses) {
            var match = horse.GetMatch<Horse>();
            this.Options.Add(new ($"Name: {horse.Name} | {horse.Energy} {(horse.IsInMarket ? "| Selling on market" : "")}", () => this.Menu.AddView(new TeamHorseInspectView(horse)), horse.Status == Status.Racing));
        }

        return true;
    }

}
