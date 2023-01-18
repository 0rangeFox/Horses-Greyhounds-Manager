using System.Collections.ObjectModel;
using System.Runtime.Serialization.Formatters.Binary;
using HaGManager.Models;
using SysFile = System.IO.File;

namespace HaGManager.Helpers;

public static class File {

    private static readonly string SaveFilePath = $"{Directory.GetCurrentDirectory()}/save.hg";

    [Serializable]
    public class GameFile {

        public int Day { get; }
        public ReadOnlyCollection<Team> Teams { get; }
        public Queue<Team> ShuffledPlayTeam { get; }
        public List<ISeller<Animal>> Market { get; }
        public List<IMatch<Animal>> Matches { get; }
        public List<ITrade<Animal>> Trades { get; }

        public GameFile(int day, ReadOnlyCollection<Team> teams, Queue<Team>? shuffledPlayTeam = null, List<ISeller<Animal>>? market = null, List<IMatch<Animal>>? matches = null, List<ITrade<Animal>>? trades = null) {
            this.Day = day;
            this.Teams = teams;
            this.ShuffledPlayTeam = shuffledPlayTeam ?? new Queue<Team>();
            this.Market = market ?? new List<ISeller<Animal>>();
            this.Matches = matches ?? new List<IMatch<Animal>>();
            this.Trades = trades ?? new List<ITrade<Animal>>();
        }

    }

    public class SaveGame {

        public GameFile File { get; }
        public DateTime LastTime { get; }

        public SaveGame(GameFile file, DateTime lastTime) {
            this.File = file;
            this.LastTime = lastTime;
        }

    }

    public static void Write(GameFile actualGameState) {
        using Stream stream = SysFile.Open(SaveFilePath, FileMode.Create);
        new BinaryFormatter().Serialize(stream, actualGameState);
    }

    public static SaveGame? Read() {
        try {
            using Stream stream = SysFile.Open(SaveFilePath, FileMode.Open);
            return new SaveGame((GameFile) new BinaryFormatter().Deserialize(stream), SysFile.GetLastWriteTime(SaveFilePath));
        } catch (Exception e) {
            return null;
        }
    }

}
