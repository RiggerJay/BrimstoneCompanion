using RedSpartan.BrimstoneCompanion.MauiUI;
using CommunityToolkit.Maui;

namespace RedSpartan.BrimstoneCompanion.Presentation.MauiUI;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureApplication()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        return builder.Build().ConfigurePopupLocator();
    }
}