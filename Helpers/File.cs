using System.Collections.ObjectModel;
using System.Runtime.Serialization.Formatters.Binary;
using HaGManager.Models;
using SysFile = System.IO.File;

namespace HaGManager.Helpers;

public static class File {

    private static readonly string CurrentDirectory = $"{Directory.GetCurrentDirectory()}/save.hg";

    [Serializable]
    public class GameFile {

        public int Time { get; }
        public ReadOnlyCollection<Team> Teams { get; }
        public Queue<Team> ShuffledPlayTeam { get; }
        public List<Match> Matches { get; }

        public GameFile(int? time, ReadOnlyCollection<Team> teams, Queue<Team>? shuffledPlayTeam = null, List<Match>? matches = null) {
            this.Time = time ?? 0;
            this.Teams = teams;
            this.ShuffledPlayTeam = shuffledPlayTeam ?? new Queue<Team>();
            this.Matches = matches ?? new List<Match>();
        }

    }

    public static void Write(GameFile actualGameState) {
        using Stream stream = SysFile.Open(CurrentDirectory, FileMode.Create);
        new BinaryFormatter().Serialize(stream, actualGameState);
    }

    public static GameFile Read() {
        using Stream stream = SysFile.Open(CurrentDirectory, FileMode.Open);
        return (GameFile) new BinaryFormatter().Deserialize(stream);
    }

}
