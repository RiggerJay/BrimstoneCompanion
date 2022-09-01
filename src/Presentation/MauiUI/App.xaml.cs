namespace RedSpartan.BrimstoneCompanion.Presentation.MauiUI;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
    }
}