using CommunityToolkit.Maui.Core;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.MauiUI.Pages;
using RedSpartan.BrimstoneCompanion.MauiUI.Popups;
using RedSpartan.BrimstoneCompanion.MauiUI.Services;
using RedSpartan.BrimstoneCompanion.Presentation.MauiUI;
using RedSpartan.BrimstoneCompanion.Presentation.MauiUI.Pages;
using RedSpartan.BrimstoneCompanion.MauiUI.ViewModels;
using RedSpartan.BrimstoneCompanion.Domain.Models;
using RedSpartan.BrimstoneCompanion.AppLayer.Services;

namespace RedSpartan.BrimstoneCompanion.MauiUI
{
    internal static class Bootstraper
    {
        internal static MauiAppBuilder ConfigureApplication(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddSingleton<INavigationService, MauiNavigationService>();
            mauiAppBuilder.Services.AddSingleton<ITextResource, TextResourceService>();
            mauiAppBuilder.Services.AddSingleton<ICharacterService, CharacterService>();
            mauiAppBuilder.Services.AddSingleton<IRepository<Character>, CharacterRepository>();
            mauiAppBuilder.Services.AddSingleton(FileSystem.Current);

            mauiAppBuilder.Services.AddTransient<CharacterSelectorViewModel>();
            mauiAppBuilder.Services.AddTransient<MainViewModel>();
            mauiAppBuilder.Services.AddTransient<NewCharacterViewModel>();
            mauiAppBuilder.Services.AddTransient<CharacterViewModel>();
            mauiAppBuilder.Services.AddTransient<UpdateAttributeViewModel>();
            mauiAppBuilder.Services.AddTransient<IncrementAttributeViewModel>();
            mauiAppBuilder.Services.AddTransient<CharacterNotesViewModel>();
            mauiAppBuilder.Services.AddTransient<AddFeatureViewModel>();

            mauiAppBuilder.Services.AddTransient<AppShell>();

            mauiAppBuilder.RegisterPage<MainPage>("main");
            mauiAppBuilder.RegisterPage<CharacterPage>("character");
            mauiAppBuilder.RegisterPage<CharacterSelectorPage>("characterselector");
            mauiAppBuilder.RegisterPage<CharacterNotesPage>("characternotes");

            mauiAppBuilder.RegisterPopup<NewCharacterPopup>();
            mauiAppBuilder.RegisterPopup<UpdateAttributePopup>();
            mauiAppBuilder.RegisterPopup<IncrementAttributePopup>();
            mauiAppBuilder.RegisterPopup<NewFeaturePopup>();

            return mauiAppBuilder;
        }

        internal static MauiApp ConfigurePopupLocator(this MauiApp mauiApp)
        {
            PopupLocator.ServiceProvider = mauiApp.Services;
            return mauiApp;
        }

        private static void RegisterPopup<T>(this MauiAppBuilder mauiAppBuilder)
            where T : class, IPopup
        {
            mauiAppBuilder.Services.AddTransient<T>();
        }

        private static void RegisterPage<T>(this MauiAppBuilder mauiAppBuilder, string route)
            where T : ContentPage
        {
            mauiAppBuilder.Services.AddTransient<T>();
            Routing.RegisterRoute(route, typeof(T));
        }
    }
}