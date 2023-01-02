using HaGManager.Helpers.Views;
using HaGManager.Models;

namespace HaGManager.Views; 

public class GameView : IView {

    public List<ViewOption> Options { get; }

    private Team _team;

    public GameView(Team team) {
        this._team = team;
        this.Options = new() {
            new("End Turn")
        };
    }

}
