using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Timers;

namespace Tamagotchi;

public partial class FoodPage : ContentPage, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private GameManager gameManager => DependencyService.Get<GameManager>();

    public FoodPage()
    {
        BindingContext = this;
        InitializeComponent();
        gameManager.timer.Elapsed += OnTimerElapsed;
    }

    private void OnTimerElapsed(Object sender, ElapsedEventArgs e)
    {
        OnPropertyChanged(nameof(HungerText));
    }

    public string HungerText => gameManager.MyCreature.Hunger switch
    {
        <= 0 => "Not Hungry",
        < .25f => "A little Hungry",
        < .50f => "Moderately Hungry",
        < .75f => "Hungry",
        < 1 => "Hangry",
        >= 1.0f => "Starving",
        _ => throw new ArgumentException("Not Possible", gameManager.MyCreature.Hunger.ToString())
    };

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private async void Feed(Object sender, EventArgs e)
    {
        await Button.RelScaleTo(1.5f);
        await Button.ScaleTo(1);

        if (gameManager.MyCreature.Hunger > 0.0f)
        {
            gameManager.MyCreature.Hunger -= .3f;
        }

        gameManager.UpdateCreature(gameManager.MyCreature);
        OnPropertyChanged(nameof(HungerText));
    }
}