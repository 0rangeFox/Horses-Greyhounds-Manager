using System.Runtime.Serialization.Formatters.Binary;
using HaGManager.Models;
using SysFile = System.IO.File;

namespace HaGManager.Helpers;

public static class File {

    private static readonly string CurrentDirectory = $"{Directory.GetCurrentDirectory()}/save.hg";

    [Serializable]
    public class GameFile {

        public int Time { get; }
        public List<Team> Teams { get; }
        public Queue<Team> ShuffledPlayTeam { get; }

        public GameFile(int? time, List<Team> teams, Queue<Team>? shuffledPlayTeam) {
            this.Time = time ?? 0;
            this.Teams = teams;
            this.ShuffledPlayTeam = shuffledPlayTeam ?? new Queue<Team>();
        }

    }

    public static void Write(GameFile objectToWrite) {
        using Stream stream = SysFile.Open(CurrentDirectory, FileMode.Create);
        new BinaryFormatter().Serialize(stream, objectToWrite);
    }

    public static GameFile Read() {
        using Stream stream = SysFile.Open(CurrentDirectory, FileMode.Open);
        return (GameFile) new BinaryFormatter().Deserialize(stream);
    }

}
