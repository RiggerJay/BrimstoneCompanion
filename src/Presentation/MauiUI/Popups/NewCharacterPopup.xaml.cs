using RedSpartan.BrimstoneCompanion.MauiUI.ViewModels;

namespace RedSpartan.BrimstoneCompanion.MauiUI.Popups;

public partial class NewCharacterPopup : CommunityToolkit.Maui.Views.Popup
{
    private readonly NewCharacterViewModel _viewModel;

    public NewCharacterPopup(NewCharacterViewModel viewModel)
    {
        _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
        BindingContext = _viewModel;

        InitializeComponent();
    }
}