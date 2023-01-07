namespace HaGManager.Helpers.Views; 

public abstract class View {

    public Menu Menu { get; set; } = null!;

    public List<string> Header { get; set; } = new();
    public List<ViewOption> Options { get; set; } = new();
    public List<string> Footer { get; set; } = new();

    public string? ReturnMessage { get; set; } = null;

    public virtual void RefreshView() {}

}
