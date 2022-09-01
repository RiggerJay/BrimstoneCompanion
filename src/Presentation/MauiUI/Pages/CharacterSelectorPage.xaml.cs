using RedSpartan.BrimstoneCompanion.MauiUI.Popups;
using RedSpartan.BrimstoneCompanion.Presentation.ViewModels;
using CommunityToolkit.Maui.Views;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;

namespace RedSpartan.BrimstoneCompanion.MauiUI.Pages;

public partial class CharacterSelectorPage : ContentPage
{
    private readonly CharacterSelectorViewModel _viewModel;

    public CharacterSelectorPage(CharacterSelectorViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        var popup = new NewCharacterPopup();

        var result = await this.ShowPopupAsync(popup);

        if (result is ObservableCharacter character)
        {
            _viewModel.Characters.Add(character);
        }
    }
}