namespace Tamagotchi;

public partial class ContentViewExample : ContentView
{
    public static readonly BindableProperty MyAmazingProperty = BindableProperty.Create(nameof(MyAmazing), typeof(string), typeof(ContentViewExample)/*, propertyChanging:*/);

    //Ctrl + .

    public string MyAmazing
    {
        get => GetValue(MyAmazingProperty) as string;
        set => SetValue(MyAmazingProperty, value);
    }

    public ContentViewExample()
    {
        BindingContext = this;
        InitializeComponent();
    }

    private async void FoodPage(object sender, EventArgs e)
    {
        await FoodPageButton.RelScaleTo(1.5f);
        await FoodPageButton.ScaleTo(1);

        await Navigation.PushAsync(new FoodPage());
    }

    private async void MainPage(object sender, EventArgs e)
    {
        await MainPageButton.RelScaleTo(1.5f);
        await MainPageButton.ScaleTo(1);

        await Navigation.PushAsync(new MainPage());
    }

    public async void SleepPage(object sender, EventArgs e)
    {
        await SleepPageButton.RelScaleTo(1.5f);
        await SleepPageButton.ScaleTo(1);

        await Navigation.PushAsync(new SleepPage());
    }

    public async void PlayTimePage(object sender, EventArgs e)
    {
        await PlayTimePageButton.RelScaleTo(1.5f);
        await PlayTimePageButton.ScaleTo(1);

        await Navigation.PushAsync(new PlayTimePage());
    }

    public async void HydrationPage(object sender, EventArgs e)
    {
        await HydrationPageButton.RelScaleTo(1.5f);
        await HydrationPageButton.ScaleTo(1);

        await Navigation.PushAsync(new HydrationPage());
    }
}