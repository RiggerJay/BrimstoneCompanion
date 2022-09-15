using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;
using RedSpartan.BrimstoneCompanion.Domain.Models;
using RedSpartan.BrimstoneCompanion.MauiUI.ViewModels;

namespace RedSpartan.BrimstoneCompanion.MauiUI.Popups;

public partial class EditFeaturePopup : CommunityToolkit.Maui.Views.Popup, IInitialisePopup
{
    private readonly EditFeatureViewModel _viewModel;

    public EditFeaturePopup(EditFeatureViewModel viewModel)
    {
        BindingContext = _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));

        InitializeComponent();
    }

    public bool Initialise(IDictionary<string, object> data)
    {
        if (data.ContainsKey(nameof(Feature))
            && data[nameof(Feature)] is ObservableFeature feature)
        {
            _viewModel.Feature = feature;
            return true;
        }
        return false;
    }

    protected override void OnDismissedByTappingOutsideOfPopup()
    {
        base.OnDismissedByTappingOutsideOfPopup();
        _viewModel.Reset();
    }
}