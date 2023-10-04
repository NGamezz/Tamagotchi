using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Timers;

namespace Tamagotchi;

public partial class MainPage : ContentPage, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private GameManager gameManager => DependencyService.Get<GameManager>();

    #region Status Texts
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

    public string BoredomText => gameManager.MyCreature.Boredom switch
    {
        <= 0 => "Amused",
        < .25f => "A little Bored",
        < .50f => "Moderately Bored",
        < .75f => "Bored",
        < 1 => "Very Bored",
        >= 1.0f => "Immensely Bored",
        _ => throw new ArgumentException("Not Possible", gameManager.MyCreature.Boredom.ToString())
    };

    public string LonelinessText => gameManager.MyCreature.Loneliness switch
    {
        <= 0 => "Not Lonely",
        < .25f => "A little Lonely",
        < .50f => "Moderately Lonely",
        < .75f => "Lonely",
        < 1 => "Very Lonely",
        >= 1.0f => "Dead",
        _ => throw new ArgumentException("Not Possible", gameManager.MyCreature.Loneliness.ToString())
    };

    public string StimulatedText => gameManager.MyCreature.Stimulated switch
    {
        <= 0 => "Not Stimulated",
        < .25f => "A little Stimulated",
        < .50f => "Moderately Stimulated",
        < .75f => "Stimulated",
        < 1 => "Overly Stimulated",
        >= 1.0f => "Leave me alone",
        _ => throw new ArgumentException("Not Possible", gameManager.MyCreature.Stimulated.ToString())
    };
    #endregion

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public MainPage()
    {
        BindingContext = this;

        gameManager.timer.Elapsed += UpdateAllCreatureProperties;

        InitializeComponent();

        if (gameManager.FirstRun == false)
        {
            nameInputField.IsReadOnly = true;
            nameInputField.Text = gameManager.MyCreature?.Name;
        }
    }

    private void NameInputField_Completed(object sender, EventArgs e)
    {
        gameManager.Setup(nameInputField.Text.ToLower(), SetCreatureName);
        nameInputField.IsReadOnly = true;
    }

    private void SetCreatureName()
    {
        nameInputField.Text = gameManager.MyCreature?.Name;
    }

    private void UpdateAllCreatureProperties(Object sender, ElapsedEventArgs e)
    {
        OnPropertyChanged(nameof(HungerText));
        OnPropertyChanged(nameof(ThirstText));
        OnPropertyChanged(nameof(BoredomText));
        OnPropertyChanged(nameof(TiredText));
        OnPropertyChanged(nameof(LonelinessText));
        OnPropertyChanged(nameof(StimulatedText));
    }
}