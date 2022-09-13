using RedSpartan.BrimstoneCompanion.MauiUI.ViewModels;

namespace RedSpartan.BrimstoneCompanion.MauiUI.Popups;

public partial class NewFeaturePopup : CommunityToolkit.Maui.Views.Popup
{
    public NewFeaturePopup(NewFeatureViewModel viewModel)
    {
        BindingContext = viewModel ?? throw new ArgumentNullException(nameof(viewModel));

        InitializeComponent();
    }
}