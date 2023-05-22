namespace MauiApp1;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        var navPage = new NavigationPage(new MainPage());
        navPage.BackgroundColor = Colors.Purple;
        navPage.BarTextColor = Colors.White;

        MainPage = navPage;
    }
}
