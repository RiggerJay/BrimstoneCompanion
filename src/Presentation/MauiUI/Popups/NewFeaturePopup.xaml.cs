using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;
using RedSpartan.BrimstoneCompanion.MauiUI.ViewModels;

namespace RedSpartan.BrimstoneCompanion.MauiUI.Popups;

public partial class NewFeaturePopup : CommunityToolkit.Maui.Views.Popup
{
    private readonly AddFeatureViewModel _viewModel;
    private readonly ObservableFeature _feature = ObservableFeature.New();

    public NewFeaturePopup(AddFeatureViewModel viewModel)
    {
        BindingContext = _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));

        InitializeComponent();
    }
}