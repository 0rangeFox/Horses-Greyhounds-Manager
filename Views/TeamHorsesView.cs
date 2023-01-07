using HaGManager.Helpers.Views;
using HaGManager.Models;

namespace HaGManager.Views; 

public class TeamHorsesView : View {

    private readonly Team _team = Game.Instance.ActualTeamPlaying;

    public TeamHorsesView(Action<Horse> selectHorseAction, bool removeIfRacing = false) {
        foreach (var horse in this._team.Horses) {
            this.Options.Add(new ($"Name: {horse.Name} | {horse.Energy}", () => selectHorseAction(horse), removeIfRacing && horse.Status == Status.Racing));
        }
    }

}
