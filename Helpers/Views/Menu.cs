namespace HaGManager.Helpers.Views;

public class Menu {

    private readonly Stack<IView> _views;
    private IView? _actualView;
    private int _actualIndex = 0;

    public Stack<IView> Views => this._views;

    public Menu(IView view) {
        this._views = new Stack<IView>(new []{ view });
        this.Initialize();
    }

    public Menu(ICollection<IView> views) {
        this._views = new Stack<IView>(views.Reverse());
        this.Initialize();
    }

    private void Initialize() {
        do {
            if (!this._views.TryPeek(out this._actualView)) continue;

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
            } else this._views.Pop();
        } while (this._views.Count > 0);
    }

    private void GenerateViewVisual() {
        var viewOptions = new List<ViewOption>(this._actualView?.Options ?? new List<ViewOption>()) {
            new(this._views.Count > 1 ? "Back" : "Exit")
        };
        var actualViewOption = viewOptions[this._actualIndex];

        Console.Clear();
        Console.WriteLine(Game.Instance?.Time ?? 0);
        foreach (var option in viewOptions) {
            Console.Write(option == actualViewOption ? "> " : " ");
            Console.WriteLine(option.Name);
        }
    }

}
