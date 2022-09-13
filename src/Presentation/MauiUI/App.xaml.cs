namespace RedSpartan.BrimstoneCompanion.Presentation.MauiUI;

public partial class App : Application
{
    public App(AppShell appShell)
    {
        InitializeComponent();
        MainPage = appShell;
    }
}