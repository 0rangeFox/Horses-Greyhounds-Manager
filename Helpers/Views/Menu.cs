namespace HaGManager.Helpers.Views;

public class Menu {

    public static Stack<IView> Views = new();

    public Menu() {
        do {
            var view = Views.Peek();

            // Add Back or exit
            var viewOptions = view.Options;
            viewOptions.Add(new ViewOption(Views.Count > 1 ? "Back" : "Exit", (() => Views.Pop())));

            // Set the default index of the selected item to be the first
            var index = 0;
            var removeView = false;

            // Write the menu out
            this.WriteMenu(viewOptions, viewOptions[index]);

            // Store key info in here
            ConsoleKeyInfo keyinfo;
            do {
                keyinfo = Console.ReadKey();

                // Handle each key input (down arrow will write the menu again with a different selected item)
                if (keyinfo.Key == ConsoleKey.DownArrow)
                    if (index + 1 < viewOptions.Count) {
                        index++;
                        this.WriteMenu(viewOptions, viewOptions[index]);
                    }

                if (keyinfo.Key == ConsoleKey.UpArrow)
                    if (index - 1 >= 0) {
                        index--;
                        this.WriteMenu(viewOptions, viewOptions[index]);
                    }

                // Handle different action for the option
                if (keyinfo.Key != ConsoleKey.Enter) continue;

                if (index + 1 == viewOptions.Count)
                    removeView = !removeView;

                viewOptions[index].Selected?.Invoke();
                index = 0;
            } while (!removeView);
        } while (Views.Count > 0);
    }

    private void WriteMenu(List<ViewOption> options, ViewOption selectedOption) {
        Console.Clear();

        foreach (var option in options) {
            Console.Write(option == selectedOption ? "> " : " ");
            Console.WriteLine(option.Name);
        }
    }

}
