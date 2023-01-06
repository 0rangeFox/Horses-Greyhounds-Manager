namespace HaGManager.Helpers.Views; 

public abstract class View {

    public Menu Menu { get; set; } = null!;

    public List<string> Header { get; protected init; } = new();
    public List<ViewOption> Options { get; protected init; } = new();
    public List<string> Footer { get; protected init; } = new();

    public string? ReturnMessage { get; protected init; } = null;

    public virtual void RefreshView() {}

}
