using RedSpartan.BrimstoneCompanion.MauiUI.ViewModels;

namespace RedSpartan.BrimstoneCompanion.MauiUI.Popups;

public partial class NewNotePopup : CommunityToolkit.Maui.Views.Popup
{
    public NewNotePopup(NewNoteViewModel viewModel)
    {
        BindingContext = viewModel ?? throw new ArgumentNullException(nameof(viewModel));

        InitializeComponent();
    }
}