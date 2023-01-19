using HaGManager.Extensions;
using HaGManager.Helpers.Views;
using HaGManager.Models;

namespace HaGManager.Views; 

public class TradeOfferPreviewView<A> : GView where A: Animal {

    private const int Padding = 10;

    public TradeOfferPreviewView(ITrade<A> trade, Action? successAction = null, Action? cancelAction = null, string successMessage = "Accept") {
        this.Header = new(this.GenerateAnimalStatsCompare(trade)) {
            "",
        };

        this.Options.Add(new (successMessage, successAction));

        if (cancelAction != null)
            this.Options.Add(new ("Cancel", cancelAction));
    }

    private List<string> GenerateAnimalStatsCompare(ITrade<A> trade) {
        List<string> offerVisual = new() {
            $"{' ', Padding} {"Your", 22} {"Their", 12}",
            $"{' ', Padding} {"Name", 12} | {trade.FromAnimal.Name.Center(Padding), -Padding} | {trade.ToAnimal.Name.Center(Padding), -Padding}",
            $"{' ', Padding} {"Speed", 12} | {trade.FromAnimal.Speed.ToString().Center(Padding), -Padding} | {trade.ToAnimal.Speed.ToString().Center(Padding), -Padding}",
            $"{' ', Padding} {"Resistance", 12} | {trade.FromAnimal.Resistance.ToString().Center(Padding), -Padding} | {trade.ToAnimal.Resistance.ToString().Center(Padding), -Padding}",
            $"{' ', Padding} {"Weight", 12} | {trade.FromAnimal.Weight.ToString().Center(Padding), -Padding} | {trade.ToAnimal.Weight.ToString().Center(Padding), -Padding}",
            $"{' ', Padding} {"Diseases", 12} | {(trade.FromAnimal.Diseases.Count > 0 ? string.Join(", ", trade.FromAnimal.Diseases).Center(Padding) : "Clean".Center(Padding)), -Padding} | {(trade.ToAnimal.Diseases.Count > 0 ? string.Join(", ", trade.ToAnimal.Diseases).Center(Padding) : "Clean".Center(Padding)), -Padding}"
        };

        if (trade.Amount > 0)
            offerVisual.Add($"{' ', Padding} {"Plus", 12} | {"", Padding} | {$"${trade.Amount}".Center(Padding)}");

        return offerVisual;
    }

}
