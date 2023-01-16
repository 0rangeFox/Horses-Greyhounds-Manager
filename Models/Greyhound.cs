namespace HaGManager.Models;

[Serializable]
public class Greyhound : Animal {

    public Greyhound(string name) : base(name) { }

    public override bool BuyAnimal(Team buyer) => throw new NotImplementedException();
    public override bool SellAnimal(float price, bool quickSell = false) => throw new NotImplementedException();
    public override bool Trade(Animal? animalToTrade = null, float amount = 0f) => throw new NotImplementedException();
    public override bool RemoveTrade() => throw new NotImplementedException();
    public override bool AcceptTrade(Team team, float amount = 0f) => throw new NotImplementedException();

}
