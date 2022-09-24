using Foundation;
using RedSpartan.BrimstoneCompanion.MauiUI;

namespace RedSpartan.BrimstoneCompanion.MauiUI.Platforms.MacCatalyst;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}