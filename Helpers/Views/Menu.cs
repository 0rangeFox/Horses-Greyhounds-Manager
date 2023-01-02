namespace HaGManager.Helpers.Views;

public delegate void MenuAction(Menu menu);

public class Menu {

    private readonly Stack<View> _views;
    private View? _actualView;
    private int _actualIndex = 0;
    private string? _returnMessage;
    private Dictionary<ConsoleKey, MenuAction?> _keysActions;

    public Stack<View> Views => this._views;

    public Menu(View view, string? returnMessage = null, Dictionary<ConsoleKey, MenuAction?>? customKeyActions = null) {
        this._views = new Stack<View>(new []{ view });
        this.Initialize(returnMessage, customKeyActions);
    }

    public Menu(ICollection<View> views, string? returnMessage = null, Dictionary<ConsoleKey, MenuAction?>? customKeyActions = null) {
        this._views = new Stack<View>(views.Reverse());
        this.Initialize(returnMessage, customKeyActions);
    }

    private void Initialize(string? returnMessage, Dictionary<ConsoleKey, MenuAction?>? customKeyActions) {
        this._returnMessage = returnMessage;
        this._keysActions = new Dictionary<ConsoleKey, MenuAction?>(customKeyActions ?? new Dictionary<ConsoleKey, MenuAction?>()) {
            {
                ConsoleKey.DownArrow, (_) => {
                    if (this._actualIndex < this._actualView?.Options.Count)
                        this._actualIndex++;
                }
            }, {
                ConsoleKey.UpArrow, (_) => {
                    if (this._actualIndex - 1 >= 0)
                        this._actualIndex--;
                }
            }
        };

        do {
            if (!this._views.TryPeek(out this._actualView)) continue;

            this.GenerateViewVisual();
            var keyInfo = Console.ReadKey();

            if (this._keysActions.TryGetValue(keyInfo.Key, out var keyAction))
                keyAction?.Invoke(this);

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
            new(this._views.Count > 1 ? "Back" : (this._returnMessage ?? "Exit"))
        };
        var actualViewOption = viewOptions[this._actualIndex];

        Console.Clear();
        this._actualView?.Header.ForEach(Console.WriteLine);
        foreach (var option in viewOptions) {
            Console.Write(option == actualViewOption ? "> " : " ");
            Console.WriteLine(option.Name);
        }
        this._actualView?.Footer.ForEach(Console.WriteLine);
    }

    public void Close() => this._views.Clear();

}
