namespace HaGManager.Extensions;

public static class RandomExtension {

    public static readonly Random Random = new();

    public static double NextDouble(double minValue, double maxValue, Random? random = null) {
        return (random ?? Random).NextDouble() * (maxValue - minValue) + minValue;
    }

    public static float NextSingle(float minValue, float maxValue, Random? random = null) {
        return (float) NextDouble(minValue, maxValue, random);
    }

}
