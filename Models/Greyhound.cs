namespace HaGManager.Models;

[Serializable]
public class Greyhound : Animal {

    public Greyhound(string name) : base(name) { }

    public override void BuyAnimal(Team buyer) => throw new NotImplementedException();
    public override void SellAnimal(float price, bool quickSell = false) => throw new NotImplementedException();

}
