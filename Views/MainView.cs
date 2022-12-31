using HaGManager.Helpers.Views;

namespace HaGManager.Views; 

public class MainView : IView {

    public List<ViewOption> Options { get; }

    public MainView() {
        this.Options = new() {
            new("Start new game"),
            new("Load game"),
            new("Credits")
        };
    }

}
