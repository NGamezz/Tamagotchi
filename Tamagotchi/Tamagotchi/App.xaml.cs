﻿namespace Tamagotchi;

public partial class App : Application
{
    public App()
    {
        //When implementing the online service change CreatureDataStore to RemoteCreatureDataStore
        DependencyService.RegisterSingleton<IDataStore<Creature>>(new RemoteDataStore());

        DependencyService.RegisterSingleton(new GameManager());

        InitializeComponent();
        MainPage = new AppShell();
    }

    protected override void OnSleep()
    {
        base.OnSleep();

        GameManager gameManager = DependencyService.Get<GameManager>();
        gameManager.UpdateCreature(gameManager.MyCreature);

        var sleepTime = DateTime.Now;
        Preferences.Set("SleepTime", sleepTime);
    }

    protected override void OnResume()
    {
        base.OnResume();

        var wakeTime = DateTime.Now;

        var sleepTime = Preferences.Get("SleepTime", DateTime.Now);

        var elapsed = wakeTime - sleepTime;

        GameManager gameManager = DependencyService.Get<GameManager>();
        gameManager.TimeElapsedSinceLastRun = (float)elapsed.TotalMilliseconds;
    }
}