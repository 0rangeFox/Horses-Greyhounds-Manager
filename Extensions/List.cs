namespace HaGManager.Extensions; 

public static class ListExtension {

    private static readonly Random Random = new Random();

    public static IList<T> Shuffle<T>(this IList<T> list) {
        return list.OrderBy(_ => Random.Next()).ToList();
    }

}
