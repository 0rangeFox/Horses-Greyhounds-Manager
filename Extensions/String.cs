namespace HaGManager.Extensions; 

public static class StringExtension {

    public static string Center(this string s, int pad) => pad > 0 ? s.PadLeft((pad - s.Length) / 2 + s.Length) : s;

}
