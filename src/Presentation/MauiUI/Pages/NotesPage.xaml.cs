using RedSpartan.BrimstoneCompanion.MauiUI.Utilities;
using RedSpartan.BrimstoneCompanion.MauiUI.ViewModels;

namespace RedSpartan.BrimstoneCompanion.MauiUI.Pages;

public partial class NotesPage : ContentPage
{
    public NotesPage(NotesViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
    }

    private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        await sender.Bounce();
    }
}