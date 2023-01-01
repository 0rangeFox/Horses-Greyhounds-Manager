using HaGManager.Helpers.Views;

namespace HaGManager.Views; 

public class MainView : IView {

    public List<ViewOption> Options { get; }

    public MainView() {
        this.Options = new() {
            new("Start new game"),
            new("Load game", Game.Instance.LoadGame),
            new("Credits", Credits)
        };
    }

    private void Credits() {
        Console.WriteLine("Horses and Greyhound Manager - Game");
        Console.WriteLine("Project was developed for the class OOP.");
        Console.WriteLine("");
        Console.WriteLine("Developers:");
        Console.WriteLine("16621 - John Fernandes");
        Console.WriteLine("23479 - Nuno Ribeiro");
        Console.WriteLine("23490 - Tiago Mendes");
        Console.WriteLine("26317 - Muhammad Nabeel");
        Console.WriteLine("");
        Console.WriteLine("Press any key to close.");

        Console.ReadKey();
    }

}
