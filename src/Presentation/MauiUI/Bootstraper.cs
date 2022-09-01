using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.MauiUI.Pages;
using RedSpartan.BrimstoneCompanion.MauiUI.Services;
using RedSpartan.BrimstoneCompanion.Presentation.MauiUI;
using RedSpartan.BrimstoneCompanion.Presentation.MauiUI.Pages;
using RedSpartan.BrimstoneCompanion.Presentation.ViewModels;

namespace RedSpartan.BrimstoneCompanion.MauiUI
{
    internal static class Bootstraper
    {
        internal static MauiAppBuilder Configure(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddSingleton<INavigationService, MauiNavigationService>();

            mauiAppBuilder.Services.AddTransient<CharacterSelectorViewModel>();
            mauiAppBuilder.Services.AddTransient<MainViewModel>();

            mauiAppBuilder.Services.AddTransient<CharacterSelectorPage>();
            mauiAppBuilder.Services.AddTransient<MainPage>();
            mauiAppBuilder.Services.AddTransient<AppShell>();

            Routing.RegisterRoute("main", typeof(MainPage));
            Routing.RegisterRoute("characterselector", typeof(CharacterSelectorPage));

            return mauiAppBuilder;
        }
    }
}