namespace HaGManager.Helpers.Views; 

public class ViewOption {

    public string Name { get; }
    public Action? Selected { get; }
    public bool Disabled { get; }

    public ViewOption(string name, Action? selected = null, bool isDisabled = false) {
        this.Name = name;
        this.Selected = selected;
        this.Disabled = isDisabled;
    }

}
