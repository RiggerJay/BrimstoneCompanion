using RedSpartan.BrimstoneCompanion.MauiUI.ViewModels;

namespace RedSpartan.BrimstoneCompanion.MauiUI.Pages;

public partial class CharacterSelectorPage : ContentPage
{
    public CharacterSelectorPage(CharacterSelectorViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
    }
}