using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.MauiUI.Pages;
using RedSpartan.BrimstoneCompanion.MauiUI.Popups;
using RedSpartan.BrimstoneCompanion.MauiUI.Services;
using RedSpartan.BrimstoneCompanion.MauiUI.ViewModels;
using RedSpartan.BrimstoneCompanion.Domain.Models;
using RedSpartan.BrimstoneCompanion.AppLayer.Services;
using MediatR;

namespace RedSpartan.BrimstoneCompanion.MauiUI
{
    internal static class Bootstraper
    {
        internal static MauiAppBuilder ConfigureApplication(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddMediatR(typeof(App));

            mauiAppBuilder.Services.AddSingleton<INavigationService, MauiNavigationService>();
            mauiAppBuilder.Services.AddSingleton<IAlertService, MauiNavigationService>();
            mauiAppBuilder.Services.AddSingleton<IPopupService, MauiNavigationService>();
            mauiAppBuilder.Services.AddSingleton<ITextResource, TextResourceService>();
            mauiAppBuilder.Services.AddSingleton<ICharacterService, CharacterService>();
            mauiAppBuilder.Services.AddSingleton<ApplicationState>();
            mauiAppBuilder.Services.AddSingleton<ShellViewModel>();

            mauiAppBuilder.Services.AddSingleton<IApplicationState>(x => x.GetRequiredService<ApplicationState>());
            mauiAppBuilder.Services.AddSingleton<IUpdateApplicationState>(x => x.GetRequiredService<ApplicationState>());
            mauiAppBuilder.Services.AddSingleton<IRepository<Character>, CharacterRepository>();
            mauiAppBuilder.Services.AddSingleton(FileSystem.Current);

            mauiAppBuilder.Services.AddTransient<TabAppShell>();

            mauiAppBuilder.RegisterPage<CharacterPage, CharacterViewModel>(NavigationKeys.CHARACTER);
            mauiAppBuilder.RegisterPage<CharacterSelectorPage, CharacterSelectorViewModel>(NavigationKeys.CHARACTER_SELECTOR);
            mauiAppBuilder.RegisterPage<FeaturesPage, FeaturesViewModel>(NavigationKeys.CHARACTER_FEATURES);
            mauiAppBuilder.RegisterPage<NewFeaturePage, NewFeatureViewModel>(NavigationKeys.FEATURE_CREATE);
            mauiAppBuilder.RegisterPage<EditFeaturePage, EditFeatureViewModel>(NavigationKeys.FEATURE_EDIT);
            mauiAppBuilder.RegisterPage<NotesPage, NotesViewModel>(NavigationKeys.CHARACTER_NOTES);

            mauiAppBuilder.RegisterPopup<NewCharacterPopup, NewCharacterViewModel>(NavigationKeys.CHARACTER_CREATE);
            mauiAppBuilder.RegisterPopup<UpdateAttributePopup, UpdateAttributeViewModel>(NavigationKeys.ATTRIBUTE_UPDATE);
            mauiAppBuilder.RegisterPopup<IncrementAttributePopup, IncrementAttributeViewModel>(NavigationKeys.ATTRIBUTE_INCREMENT);
            mauiAppBuilder.RegisterPopup<NewNotePopup, NewNoteViewModel>(NavigationKeys.NOTE_CREATE);
            mauiAppBuilder.RegisterPopup<EditNotePopup, EditNoteViewModel>(NavigationKeys.NOTE_EDIT);
            mauiAppBuilder.RegisterPopup<LevelUpPopup, LevelUpViewModel>(NavigationKeys.CHARACTER_LEVELUP);
            mauiAppBuilder.RegisterPopup<ExportPopup, ExportViewModel>(NavigationKeys.CHARACTER_EXPORT);
            mauiAppBuilder.RegisterPopup<ImportPopup, ImportViewModel>(NavigationKeys.CHARACTER_IMPORT);
            mauiAppBuilder.RegisterPopup<KeywordPopup, KeywordViewModel>(NavigationKeys.KEYWORD);

            return mauiAppBuilder;
        }

        internal static MauiApp ConfigurePopupLocator(this MauiApp mauiApp)
        {
            PopupLocator.ServiceProvider = mauiApp.Services;
            return mauiApp;
        }
    }
}