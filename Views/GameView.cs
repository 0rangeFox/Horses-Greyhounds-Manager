using HaGManager.Helpers.Views;
using HaGManager.Models;

namespace HaGManager.Views; 

public class GameView : View {

    private readonly Team _team;

    public GameView(Team team) {
        this._team = team;

        this.Header = new() {
            $"Day: {Game.Instance.Time}",
            $"Team: {this._team.Name}"
        };

        this.Options = new() {
            new("Check my horses", CheckHorses),
            new ("Level Up", LevelUp)
        };

        this.Footer = new() {
            "",
            $"Click on the backspace to go to the main menu."
        };
    }

    private void CheckHorses() {
        foreach (var horse in this._team.Horses) {
            Console.WriteLine($"Horse: {horse.Name} | Energy: {horse.Energy}");
        }

        Console.ReadKey();
    }

    private void LevelUp() {
        this._team.AddExperience(1000);
        Console.WriteLine($"Balance: {this._team.Balance}");
        this._team.AddExperience(2000);
        Console.WriteLine($"Balance: {this._team.Balance}");
        this._team.AddExperience(5000);
        Console.WriteLine($"Balance: {this._team.Balance}");
        Console.ReadKey();
    }

}
