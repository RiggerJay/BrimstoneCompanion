using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.MauiUI.ViewModels;

namespace RedSpartan.BrimstoneCompanion.MauiUI.Pages;

public partial class EditFeaturePage : ContentPage
{
    private readonly ITextResource _textResource;
    private readonly EditFeatureViewModel _viewModel;

    public EditFeaturePage(EditFeatureViewModel viewModel, ITextResource textResource)
    {
        _textResource = textResource ?? throw new ArgumentNullException(nameof(textResource));
        BindingContext = _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
        InitializeComponent();
    }

    public ITextResource TextResource => _textResource;

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        _viewModel.Reset();
    }
}