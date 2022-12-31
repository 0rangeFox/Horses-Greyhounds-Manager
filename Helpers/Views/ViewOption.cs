namespace HaGManager.Helpers.Views; 

public class ViewOption {

    public string Name { get; }
    public Action? Selected { get; }

    public ViewOption(string name, Action? selected = null) {
        this.Name = name;
        this.Selected = selected;
    }

}
