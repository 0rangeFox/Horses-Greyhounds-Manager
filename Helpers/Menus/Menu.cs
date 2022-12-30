namespace HaGManager.Helpers.Menus;

public class Option {

    public string Name { get; }
    public Action? Selected { get; }

    public Option(string name, Action? selected = null) {
        this.Name = name;
        this.Selected = selected;
    }

}

public class Menu {

    private List<Option> Options { get; }

    public Menu(List<Option> options) {
        this.Options = options;

        // Set the default index of the selected item to be the first
        var index = 0;

        // Write the menu out
        this.WriteMenu(this.Options, this.Options[index]);

        // Store key info in here
        ConsoleKeyInfo keyinfo;
        do {
            keyinfo = Console.ReadKey();

            // Handle each key input (down arrow will write the menu again with a different selected item)
            if (keyinfo.Key == ConsoleKey.DownArrow)
                if (index + 1 < this.Options.Count) {
                    index++;
                    this.WriteMenu(this.Options, this.Options[index]);
                }

            if (keyinfo.Key == ConsoleKey.UpArrow)
                if (index - 1 >= 0) {
                    index--;
                    this.WriteMenu(this.Options, this.Options[index]);
                }

            // Handle different action for the option
            if (keyinfo.Key == ConsoleKey.Enter) {
                this.Options[index].Selected?.Invoke();
                index = 0;
            }
        } while (keyinfo.Key != ConsoleKey.X);

        Console.ReadKey();
    }

    private void WriteMenu(List<Option> options, Option selectedOption) {
        Console.Clear();

        foreach (var option in options) {
            Console.Write(option == selectedOption ? "> " : " ");
            Console.WriteLine(option.Name);
        }
    }

}
