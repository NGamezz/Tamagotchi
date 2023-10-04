using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Timers;

namespace Tamagotchi;

public partial class SleepPage : ContentPage, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private GameManager gameManager => DependencyService.Get<GameManager>();

    public SleepPage()
    {
        BindingContext = this;
        InitializeComponent();
        OnPropertyChanged(nameof(TiredText));
        CheckForSleepButton(gameManager.MyCreature.IsAsleep);
        gameManager.timer.Elapsed += OnTimerElapsed;
    }

    private void CheckForSleepButton(bool isAsleep)
    {
        if (isAsleep)
        {
            Button.Text = "Your Tamagotchi is currently Sleeping.";
        }
        else
        {
            Button.Text = "Send your Tamagotchi to bed.";
        }
        SemanticScreenReader.Announce(Button.Text);
    }

    private void OnTimerElapsed(Object sender, ElapsedEventArgs e)
    {
        OnPropertyChanged(nameof(TiredText));
    }

    public string TiredText => gameManager.MyCreature.Tired switch
    {
        <= 0 => "Not Tired",
        < .25f => "A little Tired",
        < .50f => "Moderately Tired",
        < .75f => "Tired",
        < 1 => "Very Tired",
        >= 1.0f => "Dead",
        _ => throw new ArgumentException("Not Possible", gameManager.MyCreature.Tired.ToString())
    };

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private async void Sleep(Object sender, EventArgs e)
    {
        await Button.RelScaleTo(1.5f);
        await Button.ScaleTo(1);

        bool isAsleep = gameManager.MyCreature.IsAsleep;
        gameManager.MyCreature.IsAsleep = !isAsleep;

        CheckForSleepButton(gameManager.MyCreature.IsAsleep);

        gameManager.UpdateCreature(gameManager.MyCreature);
        OnPropertyChanged(nameof(TiredText));
    }
}