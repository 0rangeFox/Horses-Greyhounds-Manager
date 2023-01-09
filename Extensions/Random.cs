namespace HaGManager.Extensions;

public static class RandomExtension {

    public static readonly Random Random = new();

    public static double NextDouble(double minValue, double maxValue) {
        return Random.NextDouble() * (maxValue - minValue) + minValue;
    }

    public static float NextSingle(float minValue, float maxValue) {
        return (float) NextDouble(minValue, maxValue);
    }

}
