namespace RedSpartan.BrimstoneCompanion.MauiUI;

public partial class App : Application
{
    public App(TabAppShell appShell)
    {
        InitializeComponent();
        MainPage = appShell;
    }
}