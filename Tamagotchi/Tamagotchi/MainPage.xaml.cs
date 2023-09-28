using Newtonsoft.Json;
using System.ComponentModel;
using System.Timers;

namespace Tamagotchi;

public partial class MainPage : ContentPage, INotifyPropertyChanged
{
    int count = 0;

    public string HeaderTitle { get; set; } = $"Why hello there";

    private float Hunger { get; set; } = .0f;
    public string HungerText => Hunger switch
    {
        <= 0 => "Not Hungry",
        < .25f => "A little Hungry",
        < .50f => "Moderately Hungry",
        < .75f => "Hungry",
        < 1 => "Hangry",
        >= 1.0f => "Starving",
        _ => throw new ArgumentException("Not Possible", Hunger.ToString())
    };

    public Creature MyCreature { get; set; }

    public MainPage()
    {
        BindingContext = this;

        var timer = new System.Timers.Timer()
        {
            Interval = 10.0f,
            AutoReset = true,
        };

        timer.Elapsed += TimerElapsed;
        timer.Start();

        InitializeComponent();
    }

    private void TimerElapsed(object sender, ElapsedEventArgs e)
    {
        Console.WriteLine("Timer Elapsed.");
    }

    private async void NextPage(object sender, EventArgs args)
    {
        var dataStore = DependencyService.Get<IDataStore<Creature>>();

        if (Preferences.ContainsKey("CreatureId"))
        {
            string id = Preferences.Get("CreatureId", string.Empty);
            MyCreature = await dataStore.ReadItem(id);
        }
        else
        {
            MyCreature = null;
        }

        if (MyCreature == null)
        {
            MyCreature = new Creature()
            {
                Name = "Unknown",
                Loneliness = 1.0f,
                Boredom = 1.0f,
                Tired = 1.0f,
                Stimulated = 1.0f,
                Hunger = 1.0f,
                Thirst = 1.0f,
            };

            await dataStore.CreateItem(MyCreature);
        }

        Console.WriteLine(MyCreature.Name);
    }

    private void RandomTestFunction(object sender, EventArgs args)
    {
        Navigation.PushAsync(new FoodPage());
        //HeaderTitle = "Adjusted";

        Hunger += .1f;
    }

    private async void OnCounterClicked(object sender, EventArgs e)
    {
        count += 1;

        if (count == 1)
        {
            CounterBtn.Text = $"Clicked {count} time";
        }
        else
        {
            CounterBtn.Text = $"Clicked {count} times";
        }

        //50 is in a unit relative to the device's display.
        await CounterBtn.RelRotateTo(90.0, 1000, Easing.SpringIn);
        await CounterBtn.TranslateTo(.0, 50.0, 1000);
        await CounterBtn.TranslateTo(.0, -50.0, 1000);
        await CounterBtn.ScaleTo(10, 1000, Easing.BounceIn);

        SemanticScreenReader.Announce(CounterBtn.Text);
    }
}