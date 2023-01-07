namespace HaGManager.Extensions; 

public static class EnumerableExtension {

    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> list) {
        return list.OrderBy(_ => RandomExtension.Random.Next()).ToList();
    }

}
