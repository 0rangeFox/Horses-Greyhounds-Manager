using System.Collections.Immutable;

namespace HaGManager.Helpers.Views;

public delegate void MenuAction(Menu menu);

public class Menu {

    private readonly Stack<View> _views;
    private View? _actualView;
    private int _actualIndex = 0;
    private Dictionary<ConsoleKey, MenuAction?> _keysActions;

    public ImmutableStack<View> Views => ImmutableStack.CreateRange<View>(this._views);

    public Menu(View view, Dictionary<ConsoleKey, MenuAction?>? customKeyActions = null) {
        view.Menu = this;
        this._views = new Stack<View>(new []{ view });
        this.Initialize(customKeyActions);
    }

    public Menu(ICollection<View> views, Dictionary<ConsoleKey, MenuAction?>? customKeyActions = null) {
        foreach (var view in views) view.Menu = this;
        this._views = new Stack<View>(views.Reverse());
        this.Initialize(customKeyActions);
    }

    private void Initialize(Dictionary<ConsoleKey, MenuAction?>? customKeyActions) {
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

            if (this._actualIndex < this._actualView.Options.Count)
                this._actualView.Options[this._actualIndex].Selected?.Invoke();
            else this._views.Pop();
            this._actualIndex = 0;
        } while (this._views.Count > 0);
    }

    private void GenerateViewVisual() {
        this._actualView?.RefreshView();

        var viewOptions = new List<ViewOption>(this._actualView?.Options ?? new List<ViewOption>()) {
            new(this._actualView?.ReturnMessage ?? (this._views.Count > 1 ? "Back" : "Exit"))
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

    public void AddView(View view) {
        view.Menu = this;
        this._views.Push(view);
    }

    public void RemoveRecentView() {
        this._views.Pop();
    }

}
