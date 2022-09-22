using RedSpartan.BrimstoneCompanion.MauiUI.ViewModels;

namespace RedSpartan.BrimstoneCompanion.MauiUI.Popups;

public partial class ImportPopup : CommunityToolkit.Maui.Views.Popup
{
    public ImportPopup(ImportViewModel viewModel)
    {
        BindingContext = viewModel ?? throw new ArgumentNullException(nameof(viewModel));

        InitializeComponent();
    }
}