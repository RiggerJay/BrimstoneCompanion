using CommunityToolkit.Maui;
using Serilog;
using Serilog.Events;
using Log = Serilog.Log;

namespace RedSpartan.BrimstoneCompanion.MauiUI;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        SetupSerilog();

        builder.Services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));

        //builder.Logging.AddSerilog();

        builder
            .UseMauiApp<App>()
            .UseSentry(o =>
            {
                o.Dsn = "https://1e4986f763fe4a9d83d43015f868f364@o4503976295137280.ingest.sentry.io/4503976296972288";
            })
            .ConfigureApplication()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        return builder.Build().ConfigurePopupLocator();
    }

    private static void SetupSerilog()
    {
        var flushInterval = new TimeSpan(0, 0, 1);
        var file = Path.Combine(FileSystem.AppDataDirectory, "MyApp.log");

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File(file, flushToDiskInterval: flushInterval, encoding: System.Text.Encoding.UTF8, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 1)
            .CreateLogger();
    }
}