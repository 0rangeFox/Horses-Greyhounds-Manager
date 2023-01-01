using HaGManager.Extensions;
using HaGManager.Views;

namespace HaGManager.Helpers.Views;

public class Menu {

    public static readonly Stack<IView> Views = new() {
        new MainView()
    };

    private readonly IView _actualView;
    private readonly int _actualIndex = 0;

    public Menu() {
        do {
            this._actualView = Views.Peek();

            this.GenerateViewVisual();
            var keyInfo = Console.ReadKey();

            switch (keyInfo.Key) {
                case ConsoleKey.DownArrow: {
                    if (this._actualIndex < this._actualView.Options.Count)
                        this._actualIndex++;
                    break;
                }
                case ConsoleKey.UpArrow: {
                    if (this._actualIndex - 1 >= 0)
                        this._actualIndex--;
                    break;
                }
            }

            // Handle different action for the option
            if (keyInfo.Key != ConsoleKey.Enter) continue;

            if (this._actualIndex < this._actualView.Options.Count) {
                Console.Clear();
                this._actualView.Options[this._actualIndex].Selected?.Invoke();
                this._actualIndex = 0;
            } else Views.Pop();
        } while (Game.Instance.Stop = Views.Count > 0);
    }

    private void GenerateViewVisual() {
        var viewOptions = new List<ViewOption>(this._actualView.Options) {
            new(Views.Count > 1 ? "Back" : "Exit")
        };
        var actualViewOption = viewOptions[this._actualIndex];

        Console.Clear();
        Console.WriteLine($"Day: {Game.Instance.Time}");
        foreach (var option in viewOptions) {
            Console.Write(option == actualViewOption ? "> " : " ");
            Console.WriteLine(option.Name);
        }
    }

}
