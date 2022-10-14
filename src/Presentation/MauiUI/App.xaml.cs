using Microsoft.Extensions.Logging;

namespace RedSpartan.BrimstoneCompanion.MauiUI;

public partial class App : Application
{
    public App(TabAppShell appShell, ILogger<App> logger)
    {
        AppDomain.CurrentDomain.UnhandledException += (sender, error) =>
        {
            Exception ex = error.ExceptionObject as Exception;
            logger.LogError(ex.Message, ex);
        };
        InitializeComponent();
        MainPage = appShell;
    }
}