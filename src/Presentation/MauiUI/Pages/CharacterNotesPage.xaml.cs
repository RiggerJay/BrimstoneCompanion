using RedSpartan.BrimstoneCompanion.MauiUI.ViewModels;

namespace RedSpartan.BrimstoneCompanion.MauiUI.Pages;

public partial class CharacterNotesPage : ContentPage
{
    public CharacterNotesPage(CharacterViewModel viewModel)
    {
        BindingContext = viewModel;

        InitializeComponent();
    }
}