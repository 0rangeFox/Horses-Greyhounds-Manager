using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text.RegularExpressions;
using HaGManager.Extensions;

namespace HaGManager.Models;

public interface IMatch<out A> where A : Animal {

    public Guid ID { get; }
    public string ShortID { get; }

    public int Capacity { get; }
    public int Checkpoints { get; } // 1 Checkpoint = 100 Meters
    public int CreatedDate { get; }
    public int Duration { get; }
    public int RemainingDays { get; }

    public void Start();

}

[Serializable]
public class Match<A> : IMatch<A> where A: Animal {

    private static Comparison<int> _sortDescending = new((a, b) => b.CompareTo(a));

    private static int _minPlayers = 2;
    private static int _maxPlayers = 6;
    private static int _minCheckpoints = 1;
    private static int _maxCheckpoints = 9;
    private static int _maxDuration = 14; // Days
    private static int _perTrackCheckpointDistance = 6;

    private static int _minMoneyAmount = 100;
    private static int _maxMoneyAmount = 2500;
    private static int _minExperienceAmount = 25;
    private static int _maxExperienceAmount = 150;

    public Guid ID { get; } = Guid.NewGuid();
    // Took the code from https://stackoverflow.com/a/42026123/9379900
    public string ShortID => Regex.Replace(Convert.ToBase64String(this.ID.ToByteArray()), "[/+=]", "");

    private List<A> _animals;
    public ReadOnlyCollection<A> Animals => this._animals.AsReadOnly();
    public int Capacity => this._animals.Capacity;
    public bool IsFull => this._animals.Count >= this.Capacity;
    public int Checkpoints { get; }
    public int CreatedDate { get; }
    public int Duration { get; }
    public int RemainingDays => (this.CreatedDate + this.Duration) - Game.Instance.Day;

    public ReadOnlyCollection<int> MoneyRewards;
    public ReadOnlyCollection<int> ExperienceRewards;

    public Match() {
        this._animals = new List<A>(RandomExtension.Random.Next(_minPlayers, _maxPlayers));
        this.Checkpoints = RandomExtension.Random.Next(_minCheckpoints, _maxCheckpoints);
        this.CreatedDate = Game.Instance.Day;
        this.Duration = RandomExtension.Random.Next(1, _maxDuration);

        var moneyRewards = new List<int>();
        var experienceRewards = new List<int>();
        for (int i = 0; i < this._animals.Capacity; i++) {
            moneyRewards.Add(RandomExtension.Random.Next(_minMoneyAmount, _maxMoneyAmount));
            experienceRewards.Add(RandomExtension.Random.Next(_minExperienceAmount, _maxExperienceAmount));
        }
        moneyRewards.Sort(_sortDescending);
        experienceRewards.Sort(_sortDescending);

        this.MoneyRewards = moneyRewards.AsReadOnly();
        this.ExperienceRewards = experienceRewards.AsReadOnly();
    }

    public bool AddAnimal(A animal) {
        if (this.IsFull)
            return false;

        animal.Status = Status.Racing;
        this._animals.Add(animal);

        return true;
    }

    public bool RemoveAnimal(A? animal) {
        if (!this._animals.Remove(animal))
            return false;

        animal.Status = Status.None;

        return true;
    }

    private string GenerateTrack() {
        var trackRoad = new string('.', _perTrackCheckpointDistance - 1);
        var track = "S";
        for (int i = 1; i <= this.Checkpoints; i++)
            track += $"{trackRoad}{i}";
        return track + $"{trackRoad}F";
    }

    private void GenerateRaceView(string track, int trackDistance, List<Racer> racers) {
        Console.Clear();
        foreach (var racer in racers) {
            for (int i = 0; i <= trackDistance; i++)
                Console.Write(racer.Distance == i ? '#' : '.');
            Console.WriteLine($" | {racer.Position} | {racer.Name} {(racer.FinishTime != null ? (racer.Animal.Diseases.Contains(Disease.BrokenBone) ? "| DNF" : $"| Finished in {racer.FinishTime}") : $"| Speed: {racer.Speed}")}");
        }
        Console.WriteLine(track);
    }

    public void Start() {
        if (this.RemainingDays - 1 >= 1 && !this.IsFull) return;

        var gameTime = new Stopwatch();
        var gameTrack = this.GenerateTrack();
        var gameTrackDistance = gameTrack.Length - 1;
        var racers = this._animals.Select((animal, i) => new Racer(animal, i + 1)).ToList();

        gameTime.Start();
        for (int i = 3 - 1; i > 0; i--) {
            this.GenerateRaceView(gameTrack, gameTrackDistance, racers);
            Console.WriteLine($"\nRace will start in {i}");
            Thread.Sleep(1000);
        }
        do {
            foreach (var racer in racers) {
                if (racer.FinishTime != null || racer.Distance >= gameTrackDistance) continue;

                if (racer.Speed < RandomExtension.NextDouble(0, 50)) continue;

                racer.Distance++;

                if (racer.Distance == gameTrackDistance)
                    racer.FinishTime = gameTime.Elapsed;
                else if (racer.Distance > _perTrackCheckpointDistance && (racer.Distance % _perTrackCheckpointDistance) - 1 == 0) {
                    if (RandomExtension.Random.Next(100) > 30) {
                        var diseases = Enum.GetValues<Disease>();
                        var disease = diseases[RandomExtension.Random.Next(diseases.Length)];

                        if (disease == Disease.BrokenBone)
                            racer.FinishTime = gameTime.Elapsed;
                        racer.Animal.Diseases.Add(disease);
                    }
                }
            }

            racers.OrderByDescending(racer => racer.Distance).ThenBy(racer => racer.FinishTime).ToList().ForEach((racer, pos) => racer.Position = pos + 1);

            this.GenerateRaceView(gameTrack, gameTrackDistance, racers);
            Thread.Sleep(250);
        } while (racers.Sum(racer => racer.FinishTime == null ? racer.Distance : gameTrackDistance) != racers.Count * gameTrackDistance);
        gameTime.Stop();

        Console.ReadKey();

        this.End(racers);
    }

    private void End(List<Racer> racers) {
        for (int i = 0; i < racers.Count; i++) {
            var racer = racers[i];

            this.RemoveAnimal(racer.Animal);

            racer.Team.AddMoney(this.MoneyRewards[i]);
            racer.Team.AddExperience(this.ExperienceRewards[i]);
        }

        Game.Instance.Matches.Remove(this);
    }

    public override string ToString() => $"Match ID: {this.ShortID} | Track range: {this.Checkpoints * 100} meters | Players: {this.Animals.Count}/{this.Capacity} | {(this.RemainingDays > 1 ? $"Subscriptions will close in {this.RemainingDays} days" : "Last day to subscribe")}.";
    public override int GetHashCode() => this.ID.GetHashCode();
    public override bool Equals(object? obj) => obj is Match<A> match && this.ID.Equals(match.ID);

    private class Racer {

        public A Animal { get; }
        public int Position { get; set; }
        public int Distance { get; set; } = 0;
        public TimeSpan? FinishTime { get; set; } = null;

        public Team Team => this.Animal.Team;
        public string Name => this.Team.Name;
        public double Speed => this.Animal.Speed * (this.Animal.Resistance / (this.Distance * .1)) / this.Animal.Weight;

        public Racer(A animal, int position) {
            this.Animal = animal;
            this.Position = position;
        }

    }

}
