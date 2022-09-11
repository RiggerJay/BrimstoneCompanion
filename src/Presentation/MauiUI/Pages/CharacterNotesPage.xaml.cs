using RedSpartan.BrimstoneCompanion.MauiUI.ViewModels;

namespace RedSpartan.BrimstoneCompanion.MauiUI.Pages;

public partial class CharacterNotesPage : ContentPage
{
    private CharacterNotesViewModel _viewModel;

    public CharacterNotesPage(CharacterNotesViewModel viewModel)
    {
        BindingContext = _viewModel = viewModel;

        InitializeComponent();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        _viewModel.AddFeatureCommand.Execute(this);
    }
}