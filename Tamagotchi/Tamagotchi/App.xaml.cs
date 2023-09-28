namespace Tamagotchi;

public partial class App : Application
{
    public App()
    {
        //When implementing the online service change CreatureDataStore to RemoteCreatureDataStore
        DependencyService.RegisterSingleton<IDataStore<Creature>>(new RemoteDataStore());

        InitializeComponent();

        MainPage = new AppShell();
    }

    protected override void OnSleep()
    {
        base.OnSleep();

        var sleepTime = DateTime.Now;
        Preferences.Set("SleepTime", sleepTime);
    }

    protected override void OnResume()
    {
        base.OnResume();

        var wakeTime = DateTime.Now;

        var sleepTime = Preferences.Get("SleepTime", DateTime.Now);

        var elapsed = wakeTime - sleepTime;
        var secondsPassed = elapsed.TotalSeconds;
    }
}