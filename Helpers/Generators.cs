using HaGManager.Extensions;

namespace HaGManager.Helpers; 

public static class Generators {

    private static string[] _consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "sh", "zh", "t", "v", "w", "x" };
    private static string[] _vowels = { "a", "e", "i", "o", "u", "ae", "y" };

    public static string GenerateName(int len) {
        var name = _consonants[RandomExtension.Random.Next(_consonants.Length)].ToUpper();
        name += _vowels[RandomExtension.Random.Next(_vowels.Length)];

        var b = 2; //b tells how many times a new letter has been added. It's 2 right now because the first two letters are already in the name.
        while (b < len) {
            name += _consonants[RandomExtension.Random.Next(_consonants.Length)];
            b++;
            name += _vowels[RandomExtension.Random.Next(_vowels.Length)];
            b++;
        }

        return name;
    }

}
