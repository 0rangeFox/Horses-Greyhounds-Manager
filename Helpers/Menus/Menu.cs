namespace HaGManager.Helpers.Menus; 

public class Option
{
    public string Name { get; }
    public Action? Selected { get; }

    public Option(string name, Action? selected = null)
    {
        Name = name;
        Selected = selected;
    }
}

public class Menu {

    private List<Option> Options { get; }

    private void WriteMenu(List<Option> options, Option selectedOption) {
        Console.Clear();

        foreach (Option option in options) {
            Console.Write(option == selectedOption ? "> " : " ");
            Console.WriteLine(option.Name);
        }
    }

    public Menu(List<Option> options) {
        this.Options = options;
        
        // Set the default index of the selected item to be the first
        int index = 0;

        // Write the menu out
        WriteMenu(Options, Options[index]);

        // Store key info in here
        ConsoleKeyInfo keyinfo;
        do
        {
            keyinfo = Console.ReadKey();

            // Handle each key input (down arrow will write the menu again with a different selected item)
            if (keyinfo.Key == ConsoleKey.DownArrow)
            {
                if (index + 1 < Options.Count)
                {
                    index++;
                    WriteMenu(Options, Options[index]);
                }
            }
            if (keyinfo.Key == ConsoleKey.UpArrow)
            {
                if (index - 1 >= 0)
                {
                    index--;
                    WriteMenu(Options, Options[index]);
                }
            }
            // Handle different action for the option
            if (keyinfo.Key == ConsoleKey.Enter)
            {
                Options[index].Selected?.Invoke();
                index = 0;
            }
        } while (keyinfo.Key != ConsoleKey.X);

        Console.ReadKey();
    }

}