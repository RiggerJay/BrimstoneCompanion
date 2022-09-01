using RedSpartan.BrimstoneCompanion.MauiUI.Popups;
using RedSpartan.BrimstoneCompanion.Presentation.ViewModels;
using CommunityToolkit.Maui.Views;

namespace RedSpartan.BrimstoneCompanion.MauiUI.Pages;

public partial class CharacterSelectorPage : ContentPage
{
    public CharacterSelectorPage(CharacterSelectorViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        var popup = new NewCharacterPopup();

        this.ShowPopup(popup);
    }
}