using HaGManager.Helpers.Views;
using HaGManager.Models;

namespace HaGManager.Views; 

public class TradeOfferPreviewView<A> : View where A: Animal {

    static string Centered(int total, string s) => s.PadLeft((total - s.Length) / 2 + s.Length);
    static string Centered(int total, float s) => Centered(total, s.ToString());

    public TradeOfferPreviewView(ITrade<A> trade, Action? successAction = null, string successMessage = "Accept") {
        this.Header = new(this.GenerateAnimalStatsCompare(trade)) {
            "",
        };

        this.Options.Add(new (successMessage, successAction));
        this.ReturnMessage = "Cancel";
    }

    private List<string> GenerateAnimalStatsCompare(ITrade<A> trade) {
        List<string> offerVisual = new() {
            $"{' ', 10} {"Your", 22} {"Their", 12}",
            $"{' ', 10} {"Name", 12} | {Centered(10, trade.FromAnimal.Name), -10} | {Centered(10, trade.ToAnimal.Name), -10}",
            $"{' ', 10} {"Speed", 12} | {Centered(10, trade.FromAnimal.Speed), -10} | {Centered(10, trade.ToAnimal.Speed), -10}",
            $"{' ', 10} {"Resistance", 12} | {Centered(10, trade.FromAnimal.Resistance), -10} | {Centered(10, trade.ToAnimal.Resistance), -10}",
            $"{' ', 10} {"Weight", 12} | {Centered(10, trade.FromAnimal.Weight), -10} | {Centered(10, trade.ToAnimal.Weight), -10}",
            $"{' ', 10} {"Diseases", 12} | {(trade.FromAnimal.Diseases.Count > 0 ? Centered(10, string.Join(", ", trade.FromAnimal.Diseases)) : Centered(10, "Clean")), -10} | {(trade.ToAnimal.Diseases.Count > 0 ? Centered(10, string.Join(", ", trade.ToAnimal.Diseases)) : Centered(10, "Clean")), -10}"
        };

        if (trade.Amount > 0)
            offerVisual.Add($"{' ', 10} {"Plus", 12} | {"", 10} | {Centered(10, $"${trade.Amount}")}");

        return offerVisual;
    }

}
