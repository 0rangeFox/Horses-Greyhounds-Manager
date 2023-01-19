using HaGManager.Models;

namespace HaGManager.Helpers.Views; 

public abstract class View {

    public Menu Menu { get; set; } = null!;
    protected Team Team { get; init; } = Game.Instance.ActualTeamPlaying;

    public List<string> Header { get; set; } = new();
    public List<ViewOption> Options { get; set; } = new();
    public List<string> Footer { get; set; } = new();

    public string? ReturnMessage { get; set; } = null;

    public virtual bool RefreshView() => true;

}
