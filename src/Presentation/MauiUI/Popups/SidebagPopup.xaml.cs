using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;
using RedSpartan.BrimstoneCompanion.Domain.Models;
using RedSpartan.BrimstoneCompanion.MauiUI.ViewModels;

namespace RedSpartan.BrimstoneCompanion.MauiUI.Popups;

public partial class SidebagPopup : CommunityToolkit.Maui.Views.Popup
{
    private readonly LevelUpViewModel _viewModel;

    public SidebagPopup(LevelUpViewModel viewModel)
    {
        BindingContext = _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));

        InitializeComponent();
    }

    protected override void OnDismissedByTappingOutsideOfPopup()
    {
        base.OnDismissedByTappingOutsideOfPopup();
    }
}