using RedSpartan.BrimstoneCompanion.MauiUI.Utilities;
using RedSpartan.BrimstoneCompanion.MauiUI.ViewModels;

namespace RedSpartan.BrimstoneCompanion.MauiUI.Popups;

public partial class SidebagPopup : CommunityToolkit.Maui.Views.Popup
{
    private readonly SidebagViewModel _viewModel;

    public SidebagPopup(SidebagViewModel viewModel)
    {
        BindingContext = _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));

        InitializeComponent();
    }

    protected override void OnDismissedByTappingOutsideOfPopup()
    {
        base.OnDismissedByTappingOutsideOfPopup();

        _viewModel.Reset();
    }

    private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        await sender.Bounce();
    }
}