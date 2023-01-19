using HaGManager.Helpers.Views;
using HaGManager.Models;

namespace HaGManager.Views; 

public class MatchesView : GView {

    public MatchesView() {
        this.Header = new() {
            $"Day: {Game.Instance.Day}",
            $"Team: {this.Team.Name}",
            $"Total Matches: {Game.Instance.Matches.Count}",
            ""
        };

        this.ReturnMessage = "Return to menu";
    }

    public override bool RefreshView() {
        List<ViewOption> horsesOptions = new(), greyhoundOptions = new();
        foreach (var gameMatch in Game.Instance.Matches) {
            switch (gameMatch) {
                case Match<Horse> match:
                    horsesOptions.Add(new ViewOption(match.ToString(), () => this.Menu.AddView(new HorseMatchView(match))));
                    break;
                case Match<Greyhound> match:
                    greyhoundOptions.Add(new ViewOption(match.ToString(), () => this.Menu.AddView(new GreyhoundMatchView(match))));
                    break;
            }
        }

        this.Options.Clear();
        this.Options.Add(new ViewOption($"- Horses ({horsesOptions.Count}x matches)", null, true));
        this.Options.AddRange(horsesOptions);
        this.Options.Add(new ViewOption("", null, true));
        this.Options.Add(new ViewOption($"- Greyhounds ({greyhoundOptions.Count}x matches)", null, true));
        this.Options.AddRange(greyhoundOptions);
        this.Options.Add(new ViewOption("", null, true));

        return true;
    }

}
