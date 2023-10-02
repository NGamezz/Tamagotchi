using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Timers;

namespace Tamagotchi;

public partial class PlayTimePage : ContentPage, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private GameManager gameManager => DependencyService.Get<GameManager>();

    public PlayTimePage()
    {
        BindingContext = this;
        InitializeComponent();
        gameManager.timer.Elapsed += OnTimerElapsed;
    }

    private void OnTimerElapsed(Object sender, ElapsedEventArgs e)
    {
        OnPropertyChanged(nameof(BoredomText));
    }

    public string BoredomText => gameManager.MyCreature.Boredom switch
    {
        <= 0 => "Amused",
        < .25f => "A little Bored",
        < .50f => "Moderately Bored",
        < .75f => "Bored",
        < 1 => "Very Bored",
        >= 1.0f => "Immensely Bored",
        _ => throw new ArgumentException("Not Possible", gameManager.MyCreature.Hunger.ToString())
    };

    //Add name selection for the creature.

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private async void PlayWithTamagotchi(Object sender, EventArgs e)
    {
        await Button.RelScaleTo(1.5f);
        await Button.ScaleTo(1);

        if (gameManager.MyCreature.Boredom > 0.0f)
        {
            gameManager.MyCreature.Boredom -= .2f;
        }
        if (gameManager.MyCreature.Stimulated <= 1.0f)
        {
            gameManager.MyCreature.Stimulated += .2f;
        }
        if (gameManager.MyCreature.Hunger <= 1.0f)
        {
            gameManager.MyCreature.Hunger += .1f;
        }
        if (gameManager.MyCreature.Loneliness > 0)
        {
            gameManager.MyCreature.Loneliness -= .1f;
        }

        gameManager.UpdateCreature(gameManager.MyCreature);
        OnPropertyChanged(nameof(BoredomText));
    }
}