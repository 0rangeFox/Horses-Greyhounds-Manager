using HaGManager.Helpers.Views;
using HaGManager.Models;

namespace HaGManager.Views; 

public class TeamGreyhoundsView : View {

    public TeamGreyhoundsView(Action<Greyhound> selectGreyhoundAction, bool removeIfRacing = false) {
        var greyhound = this.Team.Greyhound;
        var match = this.Team.Greyhound.GetMatch<Greyhound>();
        this.Options.Add(new ($"Name: {greyhound.Name} {(match != null ? $"| Preparing on match {match.ShortID}" : "")}", () => selectGreyhoundAction(greyhound), removeIfRacing && greyhound.IsInRace));
    }

}
