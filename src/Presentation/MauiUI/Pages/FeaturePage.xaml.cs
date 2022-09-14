using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.MauiUI.Utilities;
using RedSpartan.BrimstoneCompanion.MauiUI.ViewModels;

namespace RedSpartan.BrimstoneCompanion.MauiUI.Pages;

public partial class FeaturePage : ContentPage
{
    private readonly ITextResource _textResource;

    public FeaturePage(FeatureViewModel viewModel
        , ITextResource textResource)
    {
        _textResource = textResource ?? throw new ArgumentNullException(nameof(textResource));
        BindingContext = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
        InitializeComponent();
    }

    public ITextResource TextResource => _textResource;

    private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        await sender.Bounce();
    }
}