namespace HaGManager.Helpers.Views; 

public abstract class View {

    public List<ViewOption> Options { get; protected init; } = new();
    public List<string> Header { get; protected init; } = new();
    public List<string> Footer { get; protected init; } = new();

}
