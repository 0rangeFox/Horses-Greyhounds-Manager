using HaGManager.Helpers.Views;
using HaGManager.Models;

namespace HaGManager.Views; 

public class TeamGreyhoundsView : View {

    private readonly Team _team = Game.Instance.ActualTeamPlaying;

    public TeamGreyhoundsView(Action<Greyhound> selectGreyhoundAction, bool removeIfRacing = false) {
        var greyhound = this._team.Greyhound;
        var match = this._team.Greyhound.GetMatch<Greyhound>();
        this.Options.Add(new ($"Name: {greyhound.Name} {(match != null ? $"| Preparing on match {match.ShortID}" : "")}", () => selectGreyhoundAction(greyhound), removeIfRacing && greyhound.Status == Status.Racing));
    }

}
