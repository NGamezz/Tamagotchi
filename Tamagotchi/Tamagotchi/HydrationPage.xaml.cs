using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Timers;

namespace Tamagotchi;

public partial class HydrationPage : ContentPage, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private GameManager gameManager => DependencyService.Get<GameManager>();

    public HydrationPage()
    {
        BindingContext = this;
        InitializeComponent();
        gameManager.timer.Elapsed += OnTimerElapsed;
    }

    private void OnTimerElapsed(Object sender, ElapsedEventArgs e)
    {
        OnPropertyChanged(nameof(ThirstText));
    }

    public string ThirstText => gameManager.MyCreature.Thirst switch
    {
        <= 0 => "Not Thirsty",
        < .25f => "A little Thirsty",
        < .50f => "Moderately Thirsty",
        < .75f => "Thirsty",
        < 1 => "Very Thirsty",
        >= 1.0f => "Diedration",
        _ => throw new ArgumentException("Not Possible", gameManager.MyCreature.Thirst.ToString())
    };

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private async void Hydrate(Object sender, EventArgs e)
    {
        await Button.RelScaleTo(1.5f);
        await Button.ScaleTo(1);

        if (gameManager.MyCreature.Thirst > 0.0f)
        {
            gameManager.MyCreature.Thirst -= .3f;
        }

        gameManager.UpdateCreature(gameManager.MyCreature);
        OnPropertyChanged(nameof(ThirstText));
    }
}