using HaGManager.Extensions;

namespace HaGManager.Models; 

public enum Event {

    None,
    DoubleXP,
    DoubleMoney,
    SkipDay,
    StaffStrike,
    MarketStrike,
    MarketDiscount25,
    MarketDiscount50,
    MarketDiscount75

}

public static class EventExtension {

    public static Event GetRandomEvent(this Event @event) {
        var events = Enum.GetValues<Event>();
        return events[RandomExtension.Random.Next(events.Length - 1)];
    }

    public static string GetString(this Event @event) {
        return @event switch {
            Event.DoubleXP => "Double XP",
            Event.DoubleMoney => "Double Money",
            Event.SkipDay => "Skip Day",
            Event.StaffStrike => "Staff Strike",
            Event.MarketStrike => "Market Strike",
            Event.MarketDiscount25 => "Market 25% Discount",
            Event.MarketDiscount50 => "Market 50% Discount",
            Event.MarketDiscount75 => "Market 75% Discount",
            _ => ""
        };
    }

    public static float GetMarketDiscount(this Event @event, float price) {
        switch (@event) {
            case Event.MarketDiscount25: return price * .75f;
            case Event.MarketDiscount50: return price * .50f;
            case Event.MarketDiscount75: return price * .25f;
            default: return price;
        }
    }

    public static float GetDoubled(this Event @event, float value) =>
        @event is Event.DoubleXP or Event.DoubleMoney ? value * 2 : value;

    public static int GetDoubled(this Event @event, int value) =>
        @event is Event.DoubleXP or Event.DoubleMoney ? value * 2 : value;

}
