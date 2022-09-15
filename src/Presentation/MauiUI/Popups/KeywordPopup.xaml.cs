using RedSpartan.BrimstoneCompanion.MauiUI.ViewModels;

namespace RedSpartan.BrimstoneCompanion.MauiUI.Popups;

public partial class KeywordPopup : CommunityToolkit.Maui.Views.Popup
{
    public KeywordPopup(KeywordViewModel viewModel)
    {
        BindingContext = viewModel ?? throw new ArgumentNullException(nameof(viewModel));

        InitializeComponent();
    }
}