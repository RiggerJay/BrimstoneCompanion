using CommunityToolkit.Maui.Core;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.MauiUI.Pages;
using RedSpartan.BrimstoneCompanion.MauiUI.Popups;
using RedSpartan.BrimstoneCompanion.MauiUI.Services;
using RedSpartan.BrimstoneCompanion.Presentation.MauiUI;
using RedSpartan.BrimstoneCompanion.Presentation.MauiUI.Pages;
using RedSpartan.BrimstoneCompanion.MauiUI.ViewModels;
using RedSpartan.BrimstoneCompanion.Domain.Models;

namespace RedSpartan.BrimstoneCompanion.MauiUI
{
    internal static class Bootstraper
    {
        internal static MauiAppBuilder ConfigureApplication(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddSingleton<INavigationService, MauiNavigationService>();
            mauiAppBuilder.Services.AddSingleton<IRepository<Character>, CharacterRepository>();
            mauiAppBuilder.Services.AddSingleton(FileSystem.Current);

            mauiAppBuilder.Services.AddTransient<CharacterSelectorViewModel>();
            mauiAppBuilder.Services.AddTransient<MainViewModel>();
            mauiAppBuilder.Services.AddTransient<NewCharacterViewModel>();

            mauiAppBuilder.Services.AddTransient<CharacterSelectorPage>();
            mauiAppBuilder.Services.AddTransient<MainPage>();
            mauiAppBuilder.Services.AddTransient<AppShell>();

            mauiAppBuilder.RegisterPopup<NewCharacterPopup>("NewCharacter");

            Routing.RegisterRoute("main", typeof(MainPage));
            Routing.RegisterRoute("characterselector", typeof(CharacterSelectorPage));

            return mauiAppBuilder;
        }

        internal static MauiApp ConfigurePopupLocator(this MauiApp mauiApp)
        {
            PopupLocator.ServiceProvider = mauiApp.Services;
            return mauiApp;
        }

        private static void RegisterPopup<T>(this MauiAppBuilder mauiAppBuilder, string route)
            where T : class, IPopup
        {
            mauiAppBuilder.Services.AddTransient<T>();
        }
    }
}