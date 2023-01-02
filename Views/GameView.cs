using HaGManager.Helpers.Views;
using HaGManager.Models;

namespace HaGManager.Views; 

public class GameView : View {

    private readonly Team _team;

    public GameView(Team team) {
        this._team = team;

        this.Header = new() {
            $"Day: {Game.Instance.Time}",
            $"Team: {this._team.Name}"
        };

        this.Options = new() {
            new("End Turn")
        };

        this.Footer = new() {
            "",
            $"Click on the backspace to go to the main menu."
        };
    }

}
