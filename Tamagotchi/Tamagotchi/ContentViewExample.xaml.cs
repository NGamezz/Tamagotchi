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

    private void NextPage(object sender, EventArgs e)
    {
        Navigation.PushAsync(new FoodPage());
    }

    private void MainPage(object sender, EventArgs e)
    {
        Navigation.PushAsync(new MainPage());
    }

    public void SleepPage(object sender, EventArgs e)
    {
        Navigation.PushAsync(new SleepPage());
    }

    public void PlayTimePage(object sender, EventArgs e)
    {
        Navigation.PushAsync(new PlayTimePage());
    }

    public void HydrationPage(object sender, EventArgs e)
    {
        Navigation.PushAsync(new HydrationPage());
    }
}