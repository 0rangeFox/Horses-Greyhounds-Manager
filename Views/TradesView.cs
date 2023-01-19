using System.Collections.ObjectModel;
using HaGManager.Helpers.Views;
using HaGManager.Models;

namespace HaGManager.Views; 

public class TradesView : View {

    private ReadOnlyCollection<ITrade<Animal>> Trades => Game.Instance.Trades.Where(trade => trade.ToTeam.Equals(this.Team)).ToList().AsReadOnly();
    private int TradesAmount => this.Trades.Count;

    public TradesView() {
        this.ReturnMessage = "Back to menu";
    }

    public override bool RefreshView() {
        this.Header = new() {
            $"Day: {Game.Instance.Day}",
            $"Team: {this.Team.Name}",
            $"Total of trades offers: {this.TradesAmount}",
            ""
        };

        this.Options.Clear();
        foreach (var trade in this.Trades)
            this.Options.Add(new($"Team: {trade.FromTeam.Name} | Offer: \"{trade.FromAnimal.Name}\"{(trade.Amount > 0 ? $" plus ${trade.Amount}" : "")}", () => this.Menu.AddView(new TradeOfferPreviewView<Animal>(trade, () => this.AcceptTrade(trade), () => this.CancelTrade(trade)))));

        return true;
    }

    private void AcceptTrade(ITrade<Animal> trade) {
        trade.ToAnimal.AcceptTrade(this.Team, trade.Amount);
        this.Menu.RemoveRecentView();
    }

    private void CancelTrade(ITrade<Animal> trade) {
        trade.ToAnimal.CancelTrade(trade);
        this.Menu.RemoveRecentView();
    }

}
